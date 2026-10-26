using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class AboutPage:ContentPage
{
 private readonly AboutViewModel vm;
 public AboutPage(AboutViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();}
}