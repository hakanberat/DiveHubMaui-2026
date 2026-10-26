using System.Collections.ObjectModel;
using System.Globalization;
using DiveHubMauiApp.Data;
using DiveHubMauiApp.Models;
using DiveHubMauiApp.Services;
namespace DiveHubMauiApp.ViewModels;
public sealed class LogbookViewModel : ViewModelBase
{
 private readonly IDiveRepository repository;
 private List<DiveRecord> all=[];
 private string search="",sort="Newest first";
 private int total;
 private string totalTime="0 min",maximum="0 m";
 public ObservableCollection<DiveRecord> Dives{get;}=[];
 public List<string> SortOptions{get;}=["Newest first","Oldest first","Dive number ↓","Dive number ↑"];
 public string Search{get=>search;set{if(Set(ref search,value))Apply();}}
 public string Sort{get=>sort;set{if(Set(ref sort,value))Apply();}}
 public int Total{get=>total;set=>Set(ref total,value);}
 public string TotalTime{get=>totalTime;set=>Set(ref totalTime,value);}
 public string Maximum{get=>maximum;set=>Set(ref maximum,value);}
 public bool IsEmpty=>Dives.Count==0;
 public string EmptyTitle=>all.Count==0?"Your underwater story starts here.":"No matching dives.";
 public string EmptyMessage=>all.Count==0?"Record a first dive. Every memory stays on this device.":"Try a different site, buddy, number or note.";
 public AsyncCommand Add{get;}
 public AsyncCommand Open{get;}
 public AsyncCommand Refresh{get;}
 public LogbookViewModel(IDiveRepository repository,NavigationService nav)
 {this.repository=repository;Add=Command(_=>nav.EditDiveAsync());Open=Command(p=>p is DiveRecord dive?nav.DiveAsync(dive.Id):Task.CompletedTask);Refresh=Command(_=>LoadAsync());}
 public Task LoadAsync()=>RunAsync(async()=>{
  all=await repository.GetAllAsync();var stats=LogbookQuery.Statistics(all);Total=stats.Count;
  long minutes=stats.Minutes;TotalTime=minutes>=60?$"{minutes/60} h {minutes%60} m":$"{minutes} min";
  Maximum=$"{stats.MaximumDepth:0.#} m";Apply();
 });
 private void Apply(){
  var query=LogbookQuery.Apply(all,Search,Sort);
  Dives.Clear();foreach(var d in query)Dives.Add(d);
  Notify(nameof(IsEmpty));Notify(nameof(EmptyTitle));Notify(nameof(EmptyMessage));
 }
}
public sealed class DiveDetailViewModel : ViewModelBase
{
 private readonly IDiveRepository repository;
 private DiveRecord? dive;
 public DiveRecord? Dive{get=>dive;set=>Set(ref dive,value);}
 public AsyncCommand Edit{get;}
 public DiveDetailViewModel(IDiveRepository repository,NavigationService nav){this.repository=repository;Edit=Command(_=>Dive is {} d?nav.EditDiveAsync(d.Id):Task.CompletedTask);}
 public Task LoadAsync(int id)=>RunAsync(async()=>{Dive=await repository.GetAsync(id);if(Dive is null)Error="This dive no longer exists.";});
 public Task DeleteAsync()=>RunAsync(async()=>{if(Dive is {} d){await repository.DeleteAsync(d.Id);await Shell.Current.GoToAsync("..");}});
}
public sealed class EditDiveViewModel : ViewModelBase
{
 private readonly IDiveRepository repository;
 private readonly NavigationService nav;
 private int id;
 private bool loading;
 private bool dirty;
 private string number="",site="",depth="",duration="",start="",end="",exposure="",weight="0",leader="",buddy="",notes="";
 private DateTime date=DateTime.Today;
 public string Title=>id==0?"Capture the dive.":"Refine the memory.";
 public DateTime Today=>DateTime.Today;
 public DateTime Earliest=>new(1900,1,1);
 public bool IsDirty=>dirty;
 private void Field(ref string field,string value,string name){if(Set(ref field,value,name)&&!loading)dirty=true;}
 public string Number{get=>number;set=>Field(ref number,value,nameof(Number));}
 public string Site{get=>site;set=>Field(ref site,value,nameof(Site));}
 public DateTime Date{get=>date;set{if(Set(ref date,value)&&!loading)dirty=true;}}
 public string Depth{get=>depth;set=>Field(ref depth,value,nameof(Depth));}
 public string Duration{get=>duration;set=>Field(ref duration,value,nameof(Duration));}
 public string Start{get=>start;set=>Field(ref start,value,nameof(Start));}
 public string End{get=>end;set=>Field(ref end,value,nameof(End));}
 public string Exposure{get=>exposure;set=>Field(ref exposure,value,nameof(Exposure));}
 public string Weight{get=>weight;set=>Field(ref weight,value,nameof(Weight));}
 public string Leader{get=>leader;set=>Field(ref leader,value,nameof(Leader));}
 public string Buddy{get=>buddy;set=>Field(ref buddy,value,nameof(Buddy));}
 public string Notes{get=>notes;set=>Field(ref notes,value,nameof(Notes));}
 public AsyncCommand Save{get;}
 public EditDiveViewModel(IDiveRepository repository,NavigationService nav){this.repository=repository;this.nav=nav;Save=Command(_=>SaveAsync());}
 public Task LoadAsync(int recordId,string initialSite="")=>RunAsync(async()=>{
  loading=true;
  try{
   id=recordId;
   var d=id==0?new DiveRecord{DiveNumber=await NextNumberAsync()}:await repository.GetAsync(id)??throw new InvalidOperationException("This dive no longer exists.");
   Number=d.DiveNumber.ToString(CultureInfo.InvariantCulture);Site=id==0?initialSite:d.DiveSite;Date=d.Date;
   Depth=id==0?"":d.MaxDepth.ToString(CultureInfo.CurrentCulture);Duration=id==0?"":d.Duration.ToString();
   Start=id==0?"":d.StartingPressure.ToString();End=id==0?"":d.EndingPressure.ToString();
   Exposure=d.ExposureProtection;Weight=d.Weight.ToString(CultureInfo.CurrentCulture);Leader=d.DiveLeader;Buddy=d.DiveBuddy;Notes=d.Notes;
   dirty=false;Notify(nameof(Title));
  }finally{loading=false;}
 });
 private async Task<int> NextNumberAsync(){var all=await repository.GetAllAsync();return all.Count==0?1:all.Max(d=>d.DiveNumber)+1;}
 private static int Integer(string text,string field)=>int.TryParse(text,NumberStyles.Integer,CultureInfo.CurrentCulture,out var n)?n:throw new ArgumentException($"Enter a whole number for {field}.");
 private static double Decimal(string text,string field){
  if(double.TryParse(text,NumberStyles.AllowDecimalPoint|NumberStyles.AllowLeadingSign,CultureInfo.CurrentCulture,out var n)&&double.IsFinite(n))return n;
  if(double.TryParse(text,NumberStyles.AllowDecimalPoint|NumberStyles.AllowLeadingSign,CultureInfo.InvariantCulture,out n)&&double.IsFinite(n))return n;
  throw new ArgumentException($"Enter a number for {field}.");
 }
 private async Task SaveAsync(){
  Error="";IsBusy=true;
  try{
   var d=new DiveRecord{Id=id,DiveNumber=Integer(Number,"dive number"),DiveSite=Site.Trim(),Date=Date,MaxDepth=Decimal(Depth,"depth"),Duration=Integer(Duration,"duration"),StartingPressure=Integer(Start,"starting pressure"),EndingPressure=Integer(End,"ending pressure"),ExposureProtection=Exposure.Trim(),Weight=Decimal(Weight,"weight"),DiveLeader=Leader.Trim(),DiveBuddy=Buddy.Trim(),Notes=Notes.Trim()};
   await repository.SaveAsync(d);dirty=false;await nav.BackAsync();
  }finally{IsBusy=false;}
 }
}
