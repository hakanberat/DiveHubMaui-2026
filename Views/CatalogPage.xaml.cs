using DiveHubMauiApp.Services;
using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class CatalogPage : ContentPage
{
 private readonly CatalogViewModel vm;
 public CatalogPage(CatalogViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync();}
 private void EntryInvoked(object? sender,EventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
 private void ItemTapped(object? sender,TappedEventArgs e)=>vm.Open.Execute(((BindableObject)sender!).BindingContext);
 private void CollectionSizeChanged(object? sender,EventArgs e){var span=Collection.Width>=950?3:Collection.Width>=600?2:1;if(ItemsLayout.Span!=span)ItemsLayout.Span=span;}
}
public sealed class SitesPage(ContentService content,NavigationService nav):CatalogPage(new CatalogViewModel("site",content,nav));
public sealed class CoursesPage(ContentService content,NavigationService nav):CatalogPage(new CatalogViewModel("course",content,nav));
