using DiveHubMauiApp.Models;
using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class DetailPage:ContentPage,IQueryAttributable
{
 private readonly DetailViewModel vm;
 public DetailPage(DetailViewModel vm){InitializeComponent();BindingContext=this.vm=vm;}
 public void ApplyQueryAttributes(IDictionary<string,object> query){if(query.TryGetValue("entry",out var entry)&&entry is ContentEntry e)vm.Entry=e;}
}