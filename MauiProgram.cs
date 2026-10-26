using System.Globalization;
using DiveHubMauiApp.Data;
using DiveHubMauiApp.Services;
using DiveHubMauiApp.ViewModels;
using DiveHubMauiApp.Views;
using Microsoft.Extensions.Logging;
namespace DiveHubMauiApp;
public static class MauiProgram
{
 public static MauiApp CreateMauiApp()
 {
  // Keep native date pickers, dialog labels and formatting in the app's requested language.
  CultureInfo.DefaultThreadCurrentCulture=CultureInfo.GetCultureInfo("en-GB");
  CultureInfo.DefaultThreadCurrentUICulture=CultureInfo.GetCultureInfo("en-GB");
  var builder=MauiApp.CreateBuilder();
#if WINDOWS
  Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("English", (handler,_)=>handler.PlatformView.Language="en-GB");
#endif
  builder.UseMauiApp<App>().ConfigureFonts(fonts=>{
   fonts.AddFont("OpenSans-Regular.ttf","OpenSansRegular");fonts.AddFont("OpenSans-Semibold.ttf","OpenSansSemibold");
  });
  builder.Services.AddSingleton<ContentService>();builder.Services.AddSingleton<NavigationService>();
  builder.Services.AddSingleton<IDiveRepository>(_=>new DiveRepository(Path.Combine(FileSystem.AppDataDirectory,"divehub-logbook.db3")));
  builder.Services.AddSingleton<AppShell>();
  builder.Services.AddSingleton<HomeViewModel>();builder.Services.AddSingleton<HomePage>();
  builder.Services.AddSingleton<SitesPage>();builder.Services.AddSingleton<CoursesPage>();
  builder.Services.AddSingleton<LogbookViewModel>();builder.Services.AddSingleton<LogbookPage>();
  builder.Services.AddSingleton<MoreViewModel>();builder.Services.AddSingleton<MorePage>();
  builder.Services.AddTransient<DetailViewModel>();builder.Services.AddTransient<DetailPage>();
  builder.Services.AddTransient<GalleryViewModel>();builder.Services.AddTransient<GalleryPage>();
  builder.Services.AddTransient<PhotoViewModel>();builder.Services.AddTransient<PhotoPage>();
  builder.Services.AddTransient<AboutViewModel>();builder.Services.AddTransient<AboutPage>();builder.Services.AddTransient<ContactPage>();
  builder.Services.AddTransient<DiveDetailViewModel>();builder.Services.AddTransient<DiveDetailPage>();
  builder.Services.AddTransient<EditDiveViewModel>();builder.Services.AddTransient<EditDivePage>();
#if DEBUG
  builder.Logging.AddDebug();
#endif
  return builder.Build();
 }
}