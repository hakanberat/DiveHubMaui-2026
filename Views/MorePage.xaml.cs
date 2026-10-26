using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class MorePage:ContentPage
{
 private readonly MoreViewModel vm;
 public MorePage(MoreViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();UpdateLayout(Width);}
 protected override void OnSizeAllocated(double width,double height){base.OnSizeAllocated(width,height);UpdateLayout(width);}
 private void UpdateLayout(double width){if(width<=0)return;foreach(var child in ServicesLayout.Children)FlexLayout.SetBasis((BindableObject)child,new Microsoft.Maui.Layouts.FlexBasis(width<650?1f:0.48f,true));}
 private void EntryInvoked(object? sender,EventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
 private void EntryTapped(object? sender,TappedEventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
}
