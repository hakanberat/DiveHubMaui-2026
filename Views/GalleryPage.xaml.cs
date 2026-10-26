using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class GalleryPage:ContentPage
{
 private readonly GalleryViewModel vm;
 public GalleryPage(GalleryViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();}
 private void PhotoClicked(object? sender,EventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
 private void PhotoTapped(object? sender,TappedEventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
 private void CollectionSizeChanged(object? sender,EventArgs e){var span=Collection.Width>=900?4:Collection.Width>=600?3:2;if(ItemsLayout.Span!=span)ItemsLayout.Span=span;}
}
