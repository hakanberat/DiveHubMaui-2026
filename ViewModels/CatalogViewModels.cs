using System.Collections.ObjectModel;
using DiveHubMauiApp.Models;
using DiveHubMauiApp.Services;
namespace DiveHubMauiApp.ViewModels;
public sealed class HomeViewModel : ViewModelBase
{
 private readonly ContentService content;
 public ObservableCollection<ContentEntry> Featured { get; } = [];
 public ObservableCollection<ContentEntry> Services { get; } = [];
 public ObservableCollection<GalleryPhoto> Photos { get; } = [];
 public AsyncCommand Navigate { get; }
 public AsyncCommand OpenEntry { get; }
 public AsyncCommand OpenPhoto { get; }
 public HomeViewModel(ContentService content, NavigationService nav)
 {
  this.content=content;
  Navigate=Command(p=>nav.TabAsync(p?.ToString()??"sites"));
  OpenEntry=Command(p=>p is ContentEntry entry?nav.DetailAsync(entry):Task.CompletedTask);
  OpenPhoto=Command(p=>p is GalleryPhoto photo?nav.PhotoAsync(photo):Task.CompletedTask);
 }
 public Task LoadAsync()=>RunAsync(async()=>{
  if(Featured.Count>0)return;
  var data=await content.GetAsync();
  foreach(var id in new[]{"noahsark","alp","awall"}) Featured.Add(data.Entries.First(x=>x.Id==id));
  foreach(var item in data.Entries.Where(x=>x.Kind=="service").Take(3))Services.Add(item);
  foreach(var item in data.Gallery.TakeLast(6).Take(3))Photos.Add(item);
 });
}
public sealed class CatalogViewModel : ViewModelBase
{
 private readonly ContentService content;
 private readonly string kind;
 private List<ContentEntry> all=[];
 private string search="",filter="All";
 public ObservableCollection<ContentEntry> Items { get; }=[];
 public string Title { get; }
 public string Subtitle { get; }
 public string Eyebrow { get; }
 public List<string> Filters { get; }
 public string Search { get=>search;set{if(Set(ref search,value))Apply();} }
 public string Filter { get=>filter;set{if(Set(ref filter,value))Apply();} }
 public string CountLabel => $"{Items.Count} {(kind=="site" ? (Items.Count==1 ? "location" : "locations") : (Items.Count==1 ? "course" : "courses"))} in your offline collection";
 public AsyncCommand Open { get; }
 public AsyncCommand Clear { get; }
 public CatalogViewModel(string kind,ContentService content,NavigationService nav)
 {
  this.kind=kind;this.content=content;
  Title=kind=="site"?"Find your next horizon.":"A world of possibility.";
  Subtitle=kind=="site"?"Shallows, walls and stories beneath the Mediterranean.":"Explore the training described in the Dive Hub archive.";
  Eyebrow=kind=="site"?"THE DIVE SITE COLLECTION":"LEARN & EXPLORE";
  Filters=kind=="site"?["All","Shore","Boat","Up to 18 m","Over 18 m"]:["All","PADI","CMAS","DAN","TDI"];
  Open=Command(p=>p is ContentEntry item?nav.DetailAsync(item):Task.CompletedTask);
  Clear=Command(_=>{Search="";Filter="All";return Task.CompletedTask;});
 }
 public Task LoadAsync()=>RunAsync(async()=>{all=(await content.GetAsync()).Entries.Where(e=>e.Kind==kind).ToList();Apply();});
 private void Apply()
 {
  var result=all.Where(x=>string.IsNullOrWhiteSpace(Search)||$"{x.Name} {x.Summary} {x.Description} {x.Category}".Contains(Search.Trim(),StringComparison.OrdinalIgnoreCase));
  result=Filter switch {"All"=>result,"Shore" or "Boat"=>result.Where(x=>x.Access==Filter),"Up to 18 m"=>result.Where(x=>x.MaxDepth<=18),"Over 18 m"=>result.Where(x=>x.MaxDepth>18),_=>result.Where(x=>x.Category==Filter)};
  Items.Clear();foreach(var item in result)Items.Add(item);Notify(nameof(CountLabel));
 }
}
public sealed class DetailViewModel(NavigationService nav): ViewModelBase
{
 private ContentEntry? entry;
 public ContentEntry? Entry {get=>entry;set=>Set(ref entry,value);}
 public AsyncCommand LogDive=>Command(_=>nav.EditDiveAsync(0,Entry?.Name));
 public AsyncCommand CopySource=>Command(async _=>{if(Entry is not null)await Clipboard.Default.SetTextAsync(Entry.Source);Status="Source address copied.";});
 private string status="";
 public string Status{get=>status;set=>Set(ref status,value);}
}
public sealed class MoreViewModel : ViewModelBase
{
 private readonly ContentService content;
 public ObservableCollection<ContentEntry> Services{get;}=[];
 public AsyncCommand Open{get;}
 public MoreViewModel(ContentService content,NavigationService nav){this.content=content;Open=Command(p=>p is ContentEntry e?nav.DetailAsync(e):Shell.Current.GoToAsync(p?.ToString()??"gallery"));}
 public Task LoadAsync()=>RunAsync(async()=>{if(Services.Count>0)return;foreach(var e in (await content.GetAsync()).Entries.Where(x=>x.Kind=="service"))Services.Add(e);});
}
public sealed class GalleryViewModel : ViewModelBase
{
 private readonly ContentService content;
 public ObservableCollection<GalleryPhoto> Photos{get;}=[];
 public AsyncCommand Open{get;}
 public GalleryViewModel(ContentService content,NavigationService nav){this.content=content;Open=Command(p=>p is GalleryPhoto photo?nav.PhotoAsync(photo):Task.CompletedTask);}
 public Task LoadAsync()=>RunAsync(async()=>{if(Photos.Count>0)return;foreach(var p in (await content.GetAsync()).Gallery)Photos.Add(p);});
}
public sealed class PhotoViewModel : ViewModelBase
{
 private readonly ContentService content;
 private List<GalleryPhoto> photos=[];
 private int index;
 public GalleryPhoto? Photo=>photos.Count>0?photos[index]:null;
 public string Position=>$"{(photos.Count>0?index+1:0):00} / {photos.Count:00}";
 public AsyncCommand Next{get;}
 public AsyncCommand Previous{get;}
 public PhotoViewModel(ContentService content){this.content=content;Next=Command(_=>Move(1));Previous=Command(_=>Move(-1));}
 private Task Move(int offset){if(photos.Count>0){index=(index+offset+photos.Count)%photos.Count;Notify(nameof(Photo));Notify(nameof(Position));}return Task.CompletedTask;}
 public Task LoadAsync(string id)=>RunAsync(async()=>{photos=(await content.GetAsync()).Gallery;index=Math.Max(0,photos.FindIndex(x=>x.Id==id));Notify(nameof(Photo));Notify(nameof(Position));});
}
public sealed class AboutViewModel : ViewModelBase
{
 private readonly ContentService content;
 private string about="",status="";
 public string About{get=>about;set=>Set(ref about,value);}
 public string Status{get=>status;set=>Set(ref status,value);}
 public ObservableCollection<TeamMember> Team{get;}=[];
 public ObservableCollection<ContactDetail> Contacts{get;}=[];
 public AsyncCommand Copy{get;}
 public AboutViewModel(ContentService content){this.content=content;Copy=Command(async p=>{if(p is ContactDetail d){await Clipboard.Default.SetTextAsync(d.Value);Status=$"{d.Label} copied.";}});}
 public Task LoadAsync()=>RunAsync(async()=>{var data=await content.GetAsync();About=data.About;if(Team.Count>0)return;foreach(var m in data.Team)Team.Add(m);foreach(var d in data.Contacts)Contacts.Add(d);});
}
