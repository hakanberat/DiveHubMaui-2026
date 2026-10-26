using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class EditDivePage:ContentPage,IQueryAttributable
{
 private readonly EditDiveViewModel vm;
 private int id;
 private string initialSite="";
 private bool initialized,leaving;
 public EditDivePage(EditDiveViewModel vm){
  InitializeComponent();BindingContext=this.vm=vm;
  vm.PropertyChanged+=async(_,e)=>{if(e.PropertyName==nameof(vm.Error)&&vm.HasError)await FormScroll.ScrollToAsync(0,0,true);};
 }
 public void ApplyQueryAttributes(IDictionary<string,object> query){
  if(query.TryGetValue("id",out var value))int.TryParse(value.ToString(),out id);
  if(query.TryGetValue("site",out var site))initialSite=Uri.UnescapeDataString(site.ToString()??"");
 }
 protected override async void OnAppearing(){base.OnAppearing();if(!initialized){initialized=true;await vm.LoadAsync(id,initialSite);}}
 private async Task CancelAsync(){
  if(leaving||vm.IsBusy)return;
  if(vm.IsDirty&&!await DisplayAlertAsync("Discard changes?","This dive has unsaved changes.","Discard","Keep editing"))return;
  leaving=true;try{await Shell.Current.GoToAsync("..");}catch{leaving=false;vm.Error="Unable to close the editor. Please try again.";}
 }
 private async void CancelClicked(object? sender,EventArgs e)=>await CancelAsync();
 protected override bool OnBackButtonPressed(){_ = CancelAsync();return true;}
}