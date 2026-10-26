using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class ContactPage:ContentPage
{
 private readonly AboutViewModel vm;
 public ContactPage(AboutViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();}
 private void CopyClicked(object? sender,EventArgs e)=>vm.Copy.Execute(((BindableObject)sender!).BindingContext);
}