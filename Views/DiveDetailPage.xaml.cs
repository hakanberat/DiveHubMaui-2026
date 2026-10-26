using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class DiveDetailPage:ContentPage,IQueryAttributable
{
 private readonly DiveDetailViewModel vm;
 private int id;
 public DiveDetailPage(DiveDetailViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 public void ApplyQueryAttributes(IDictionary<string,object> query){if(query.TryGetValue("id",out var value))int.TryParse(value.ToString(),out id);}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync(id);}
 private async void DeleteClicked(object? sender,EventArgs e){
  if(vm.Dive is null||vm.IsBusy)return;
  if(await DisplayAlertAsync("Delete this dive?",$"Dive {vm.Dive.DiveNumber} at {vm.Dive.DiveSite} will be permanently removed from this device.","Delete","Keep dive"))await vm.DeleteAsync();
 }
}