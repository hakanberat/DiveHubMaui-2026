using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class LogbookPage:ContentPage
{
 private readonly LogbookViewModel vm;
 public LogbookPage(LogbookViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();}
 private void DiveClicked(object? sender,EventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
 private void DiveTapped(object? sender,TappedEventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
}