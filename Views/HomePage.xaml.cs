using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class HomePage : ContentPage
{
 private readonly HomeViewModel vm;
 public HomePage(HomeViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();UpdateLayout(Width);}
 protected override void OnSizeAllocated(double width,double height){base.OnSizeAllocated(width,height);UpdateLayout(width);}
 private void UpdateLayout(double width){if(width<=0)return;RootContent.Padding=width<650?new Thickness(20,18,20,32):new Thickness(36,24,36,40);HeroTitle.FontSize=width<650?34:46;Hero.HeightRequest=width<650?420:430;foreach(var child in FeaturedLayout.Children)FlexLayout.SetBasis((BindableObject)child,new Microsoft.Maui.Layouts.FlexBasis(width<650?1f:0.32f,true));}
 private void EntryInvoked(object? sender,EventArgs e)=>vm.OpenEntry.Execute(((BindableObject)sender!).BindingContext);
 private void EntryTapped(object? sender,TappedEventArgs e)=>vm.OpenEntry.Execute(((BindableObject)sender!).BindingContext);
 private void PhotoTapped(object? sender,TappedEventArgs e)=>vm.OpenPhoto.Execute(((BindableObject)sender!).BindingContext);
 private async void GalleryClicked(object? sender,EventArgs e){try{await Shell.Current.GoToAsync("gallery");}catch{vm.Error="Unable to open the gallery. Please try again.";}}
}
