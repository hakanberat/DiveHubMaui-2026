using Microsoft.Extensions.DependencyInjection;
namespace DiveHubMauiApp;
public partial class App:Application
{
 private readonly IServiceProvider services;
 public App(IServiceProvider services){InitializeComponent();this.services=services;UserAppTheme=AppTheme.Light;}
 protected override Window CreateWindow(IActivationState? activationState)
 {
  var window=new Window(services.GetRequiredService<AppShell>()){Title="Dive Hub"};
#if WINDOWS
  var display=DeviceDisplay.MainDisplayInfo;
  window.Width=Math.Clamp(display.Width/display.Density-60,390,1280);
  window.Height=Math.Clamp(display.Height/display.Density-100,600,900);
  window.MinimumWidth=390;window.MinimumHeight=600;
#endif
  return window;
 }
}