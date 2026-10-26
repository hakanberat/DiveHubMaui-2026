using DiveHubMauiApp.Views;
using Microsoft.Extensions.DependencyInjection;
namespace DiveHubMauiApp;
public partial class AppShell:Shell
{
 public AppShell(IServiceProvider services)
 {
  InitializeComponent();
  Home.ContentTemplate=new DataTemplate(()=>services.GetRequiredService<HomePage>());
  Sites.ContentTemplate=new DataTemplate(()=>services.GetRequiredService<SitesPage>());
  Courses.ContentTemplate=new DataTemplate(()=>services.GetRequiredService<CoursesPage>());
  Logbook.ContentTemplate=new DataTemplate(()=>services.GetRequiredService<LogbookPage>());
  More.ContentTemplate=new DataTemplate(()=>services.GetRequiredService<MorePage>());
  Routing.RegisterRoute("detail",typeof(DetailPage));Routing.RegisterRoute("gallery",typeof(GalleryPage));
  Routing.RegisterRoute("photo",typeof(PhotoPage));Routing.RegisterRoute("about",typeof(AboutPage));
  Routing.RegisterRoute("contact",typeof(ContactPage));Routing.RegisterRoute("dive",typeof(DiveDetailPage));
  Routing.RegisterRoute("edit-dive",typeof(EditDivePage));
 }
}