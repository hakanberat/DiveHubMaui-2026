using System.Text.Json;
using DiveHubMauiApp.Models;
namespace DiveHubMauiApp.Services;
public sealed class ContentService
{
    private readonly Lazy<Task<Catalog>> catalog = new(LoadAsync);
    public Task<Catalog> GetAsync() => catalog.Value;
    private static async Task<Catalog> LoadAsync()
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync("catalog.json");
        return await JsonSerializer.DeserializeAsync(stream,CatalogJsonContext.Default.Catalog) ?? throw new InvalidDataException("The bundled catalog is unavailable.");
    }
}
