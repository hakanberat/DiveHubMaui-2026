namespace DiveHubMauiApp.WinUI;
public partial class App:MauiWinUIApplication
{
 public App()
 {
  Microsoft.Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride="en-US";
  InitializeComponent();
 }
 protected override MauiApp CreateMauiApp()=>MauiProgram.CreateMauiApp();
}
