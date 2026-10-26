using DiveHubMauiApp.Models;
namespace DiveHubMauiApp.Services;
public sealed class NavigationService
{
    public Task TabAsync(string tab) => Shell.Current.GoToAsync($"//{tab}");
    public Task DetailAsync(ContentEntry entry) => Shell.Current.GoToAsync("detail", new ShellNavigationQueryParameters { ["entry"] = entry });
    public Task PhotoAsync(GalleryPhoto photo) => Shell.Current.GoToAsync("photo", new ShellNavigationQueryParameters { ["photoId"] = photo.Id });
    public Task DiveAsync(int id) => Shell.Current.GoToAsync($"dive?id={id}");
    public Task EditDiveAsync(int id = 0, string? site = null) => Shell.Current.GoToAsync($"edit-dive?id={id}&site={Uri.EscapeDataString(site ?? "")}");
    public Task BackAsync() => Shell.Current.GoToAsync("..");
}

