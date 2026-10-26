using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DiveHubMauiApp.Data;
using DiveHubMauiApp.Models;
using SQLite;

var root=Path.GetFullPath(Path.Combine(AppContext.BaseDirectory,"../../../../"));
var testPath=Path.Combine(root,".reference","checks-"+Guid.NewGuid().ToString("N")+".db3");
Directory.CreateDirectory(Path.GetDirectoryName(testPath)!);
var passed=0;
void Check(bool condition,string name){if(!condition)throw new Exception("FAIL: "+name);Console.WriteLine("PASS: "+name);passed++;}
async Task Reject(Func<Task> action,string name){try{await action();}catch(ArgumentException){Check(true,name);return;}throw new Exception("Expected validation rejection: "+name);}
DiveRecord Valid(int number=1)=>new(){DiveNumber=number,DiveSite="O'Brien's reef; DROP TABLE Dives; --",Date=new DateTime(2020,5,4),MaxDepth=18.5,Duration=47,StartingPressure=210,EndingPressure=55,ExposureProtection="5 mm wetsuit",Weight=6.5,DiveLeader="Leader",DiveBuddy="Buddy",Notes="A remembered dive."};

await using(var db=new DiveRepository(testPath)){
 Check((await db.GetAllAsync()).Count==0,"New database has useful empty state");
 var d=Valid();await db.SaveAsync(d);
 Check(d.Id>0,"Insert assigns a stable record identifier");
 var loaded=(await db.GetAsync(d.Id))!;
 Check(loaded.DiveSite==d.DiveSite&&loaded.MaxDepth==18.5&&loaded.Weight==6.5&&loaded.Date==d.Date&&loaded.StartingPressure==210&&loaded.Notes==d.Notes,"All typed fields round-trip, including apostrophes and SQL-like text");
 await Reject(()=>db.SaveAsync(Valid()),"Duplicate dive numbers are rejected");
 d.DiveNumber=2;d.Notes="Updated memory";await db.SaveAsync(d);
 Check((await db.GetAsync(d.Id))!.Notes=="Updated memory"&&(await db.GetAllAsync()).Count==1,"Edit updates the existing row");
 var other=Valid(3);other.Date=new DateTime(2021,1,2);await db.SaveAsync(other);
 Check((await db.GetAllAsync())[0].DiveNumber==3,"Records sort by actual date");
 d.DiveNumber=3;await Reject(()=>db.SaveAsync(d),"Editing cannot steal another dive number");
 Check((await db.GetAsync(d.Id))!.DiveNumber==2,"Failed edit leaves the stored record unchanged");
 var bad=Valid(4);bad.EndingPressure=300;await Reject(()=>db.SaveAsync(bad),"Ending pressure cannot exceed starting pressure");
 bad=Valid(4);bad.MaxDepth=double.NaN;await Reject(()=>db.SaveAsync(bad),"Non-finite depths are rejected");
 bad=Valid(4);bad.Weight=-1;await Reject(()=>db.SaveAsync(bad),"Negative weights are rejected");
 bad=Valid(4);bad.Date=DateTime.Today.AddDays(1);await Reject(()=>db.SaveAsync(bad),"Future dates are rejected");
 bad=Valid(4);bad.DiveSite="   ";await Reject(()=>db.SaveAsync(bad),"Blank sites are rejected");
 bad=Valid(4);bad.Duration=0;await Reject(()=>db.SaveAsync(bad),"Zero duration is rejected");
 bad=Valid(4);bad.StartingPressure=0;await Reject(()=>db.SaveAsync(bad),"Zero starting pressure is rejected");
 bad=Valid(4);bad.Notes=new string('a',6001);await Reject(()=>db.SaveAsync(bad),"Oversized notes are rejected");
 bad=Valid(0);await Reject(()=>db.SaveAsync(bad),"Zero dive number is rejected");
}
await using(var db=new DiveRepository(testPath)){
 Check((await db.GetAllAsync()).Count==2,"Records survive connection close and restart");
 using var raw=new SQLiteConnection(testPath);
 var types=raw.Query<ColumnInfo>("SELECT name, type FROM pragma_table_info('Dives')").ToDictionary(x=>x.name,x=>x.type);
 Check(types["DiveNumber"].Equals("INTEGER",StringComparison.OrdinalIgnoreCase)&&types["MaxDepth"].Equals("FLOAT",StringComparison.OrdinalIgnoreCase)&&types["Date"].Equals("bigint",StringComparison.OrdinalIgnoreCase),"SQLite uses numeric and date storage types");
 Check(raw.ExecuteScalar<string>("PRAGMA integrity_check")=="ok","SQLite integrity check");
 var invalid=Valid(10);invalid.MaxDepth=-5;
 try{raw.Insert(invalid);throw new Exception("Trigger failed");}catch(SQLiteException){Check(true,"Database trigger rejects invalid direct writes");}
 var original=(await db.GetAllAsync()).First();
 await db.DeleteAsync(original.Id);
 Check(await db.GetAsync(original.Id)==null&&(await db.GetAllAsync()).Count==1,"Delete removes only its target");
}
await using(var first=new DiveRepository(testPath))
await using(var second=new DiveRepository(testPath)){
 async Task<bool> Save(IDiveRepository repo){try{await repo.SaveAsync(Valid(50));return true;}catch(ArgumentException){return false;}}
 var race=await Task.WhenAll(Save(first),Save(second));
 Check(race.Count(x=>x)==1&&(await first.GetAllAsync()).Count(d=>d.DiveNumber==50)==1,"Concurrent saves cannot create duplicate numbers");
}
using(var raw=new SQLiteConnection(testPath)){raw.Execute("PRAGMA user_version = 2");}
await using(var newer=new DiveRepository(testPath)){
 try{await newer.GetAllAsync();throw new Exception("Newer schema accepted");}catch(InvalidOperationException){Check(true,"Newer database versions are rejected without dropping data");}
}
using(var raw=new SQLiteConnection(testPath)){Check(raw.Table<DiveRecord>().Count()==2,"Schema-version rejection preserves data");}

var sample=new[]{Valid(8),Valid(2),Valid(10)};
sample[0].Date=new DateTime(2020,1,1);sample[1].Date=new DateTime(2023,1,1);sample[2].Date=new DateTime(2021,1,1);sample[2].DiveBuddy="JELLYFISH";
Check(LogbookQuery.Apply(sample," jellyfish ","Newest first").Single().DiveNumber==10,"Search matches buddies without case or surrounding-space sensitivity");
Check(LogbookQuery.Apply(sample,"not found","Newest first").Count==0,"Search produces a useful empty result");
Check(LogbookQuery.Apply(sample,"","Newest first").Select(d=>d.DiveNumber).SequenceEqual(new[]{2,10,8}),"Newest-first sort uses dates");
Check(LogbookQuery.Apply(sample,"","Oldest first").Select(d=>d.DiveNumber).SequenceEqual(new[]{8,10,2}),"Oldest-first sort uses dates");
Check(LogbookQuery.Apply(sample,"","Dive number ↑").Select(d=>d.DiveNumber).SequenceEqual(new[]{2,8,10})&&LogbookQuery.Apply(sample,"","Dive number ↓").First().DiveNumber==10,"Dive numbers sort numerically in both directions");
var stats=LogbookQuery.Statistics(sample);
Check(stats.Count==3&&stats.Minutes==141&&stats.MaximumDepth==18.5,"Dashboard totals use all records");
Check(LogbookQuery.Statistics(Array.Empty<DiveRecord>())==new DiveStatistics(0,0,0),"Empty dashboard statistics are safe");

var catalog=JsonSerializer.Deserialize(File.ReadAllText(Path.Combine(root,"Resources/Raw/catalog.json")),CatalogJsonContext.Default.Catalog)!;
Check(catalog.Entries.Count(x=>x.Kind=="site")==14,"All 14 verified dive sites are bundled");
Check(catalog.Entries.Count(x=>x.Kind=="course")==31&&new[]{"PADI","CMAS","DAN","TDI"}.All(c=>catalog.Entries.Any(x=>x.Category==c)),"31 courses across four agencies are bundled");
Check(catalog.Entries.Count(x=>x.Kind=="service")==5&&catalog.Team.Count==5&&catalog.Gallery.Count==32,"Services, team profiles and gallery are bundled");
Check(catalog.Entries.All(x=>!string.IsNullOrWhiteSpace(x.Name)&&!string.IsNullOrWhiteSpace(x.Description)&&!string.IsNullOrWhiteSpace(x.Source)),"Every catalog entry has a description and source");
var images=Directory.GetFiles(Path.Combine(root,"Resources/Images")).Select(Path.GetFileName).ToHashSet();
bool HasImage(string image)=>images.Contains(image)||images.Contains(Path.ChangeExtension(image,"svg"));
Check(catalog.Entries.All(x=>HasImage(x.Image))&&catalog.Gallery.All(x=>HasImage(x.Image))&&catalog.Team.All(x=>HasImage(x.Image)),"Every catalog image resolves locally");
Check(catalog.Entries.All(x=>!x.Image.StartsWith("http"))&&catalog.Gallery.All(x=>!x.Image.StartsWith("http")),"No catalog images load from the network");
Check(catalog.Entries.Where(x=>x.Kind=="course").All(x=>!x.Description.Contains('€')),"Historical prices are omitted");

XNamespace x="http://schemas.microsoft.com/winfx/2009/xaml";
var xaml=Directory.GetFiles(root,"*.xaml",SearchOption.AllDirectories).Where(f=>!f.Contains(Path.DirectorySeparatorChar+"obj"+Path.DirectorySeparatorChar)&&!f.Contains(Path.DirectorySeparatorChar+"bin"+Path.DirectorySeparatorChar)&&!f.Contains(".reference")).ToList();
var documents=xaml.Select(XDocument.Load).ToList();
var keys=documents.SelectMany(d=>d.Descendants().Attributes(x+"Key")).Select(a=>a.Value).ToHashSet();
foreach(var file in xaml){
 var text=File.ReadAllText(file);
 foreach(Match m in Regex.Matches(text,@"\{StaticResource ([A-Za-z][A-Za-z0-9]+)\}"))
  if(!keys.Contains(m.Groups[1].Value))throw new Exception("Missing XAML resource "+m.Groups[1].Value+" in "+file);
 foreach(Match m in Regex.Matches(text,"(?:Source|Icon)=\"([a-z][a-z0-9_]+\\.(?:png|jpg))\""))
  if(!HasImage(m.Groups[1].Value))throw new Exception("Missing XAML image "+m.Groups[1].Value);
}
Check(true,"All XAML parses and every static style, color and image reference resolves");
var appCode=Directory.GetFiles(root,"*.cs",SearchOption.AllDirectories).Where(f=>!f.Contains(".reference")&&!f.Contains(Path.DirectorySeparatorChar+"tests"+Path.DirectorySeparatorChar)&&!f.Contains(Path.DirectorySeparatorChar+"obj"+Path.DirectorySeparatorChar)&&!f.Contains(Path.DirectorySeparatorChar+"bin"+Path.DirectorySeparatorChar)).Select(File.ReadAllText);
Check(!appCode.Any(s=>s.Contains("HttpClient")||s.Contains("WebView")||s.Contains("SmtpClient")||s.Contains("WebRequest")),"Application code contains no network clients, web views or SMTP");
var manifest=XDocument.Load(Path.Combine(root,"Platforms/Android/AndroidManifest.xml"));
Check(!manifest.Descendants("uses-permission").Any(),"Android release manifest requests no network permission");
Console.WriteLine($"All {passed} checks passed.");
SQLiteAsyncConnection.ResetPool();
foreach(var file in Directory.GetFiles(Path.GetDirectoryName(testPath)!,Path.GetFileName(testPath)+"*"))File.Delete(file);
public sealed class ColumnInfo{public string name{get;set;}="";public string type{get;set;}="";}

