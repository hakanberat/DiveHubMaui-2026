using Android.App;
using Android.Content.PM;
using Android.OS;
namespace DiveHubMauiApp;
[Activity(Theme="@style/Maui.SplashTheme",MainLauncher=true,LaunchMode=LaunchMode.SingleTop,ConfigurationChanges=ConfigChanges.ScreenSize|ConfigChanges.Orientation|ConfigChanges.UiMode|ConfigChanges.ScreenLayout|ConfigChanges.SmallestScreenSize|ConfigChanges.Density)]
public class MainActivity:MauiAppCompatActivity
{
 protected override void OnCreate(Bundle? savedInstanceState)
 {
  AndroidX.AppCompat.App.AppCompatDelegate.ApplicationLocales=AndroidX.Core.OS.LocaleListCompat.ForLanguageTags("en")!;
  base.OnCreate(savedInstanceState);
 }
}