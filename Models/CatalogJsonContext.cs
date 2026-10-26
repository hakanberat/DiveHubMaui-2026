using System.Text.Json.Serialization;
namespace DiveHubMauiApp.Models;
[JsonSerializable(typeof(Catalog))]
internal partial class CatalogJsonContext:JsonSerializerContext;
