using System.Text.Json.Serialization;
namespace DiveHubMauiApp.Models;
public sealed class ContentEntry
{
    public string Id { get; set; } = "";
    public string Kind { get; set; } = "";
    public string Category { get; set; } = "";
    public string Name { get; set; } = "";
    public string Image { get; set; } = "ocean_placeholder.png";
    public string Summary { get; set; } = "";
    public string Description { get; set; } = "";
    public double? MaxDepth { get; set; }
    public string Experience { get; set; } = "Unavailable";
    public string Access { get; set; } = "Unavailable";
    public string MinimumAge { get; set; } = "Unavailable";
    public string Prerequisites { get; set; } = "Unavailable";
    public string DiveCount { get; set; } = "Unavailable";
    public string Source { get; set; } = "";
    public string SourceNote { get; set; } = "";
    [JsonIgnore] public string DepthLabel => MaxDepth is {} depth ? $"{depth:0.#} m" : "Unavailable";
    [JsonIgnore] public string Eyebrow => Kind == "site" ? $"{Access.ToUpperInvariant()} · {DepthLabel}" : Category.ToUpperInvariant();
    [JsonIgnore] public bool IsSite => Kind == "site";
    [JsonIgnore] public bool IsCourse => Kind == "course";
    [JsonIgnore] public bool HasNote => !string.IsNullOrWhiteSpace(SourceNote);
}
public sealed class GalleryPhoto
{
    public string Id { get; set; } = "";
    public string Image { get; set; } = "";
    public string Caption { get; set; } = "";
    public string Source { get; set; } = "";
}
public sealed class TeamMember
{
    public string Name { get; set; } = "";
    public string Role { get; set; } = "";
    public string Image { get; set; } = "";
}
public sealed class Catalog
{
    public List<ContentEntry> Entries { get; set; } = [];
    public List<GalleryPhoto> Gallery { get; set; } = [];
    public List<TeamMember> Team { get; set; } = [];
    public string About { get; set; } = "";
    public List<ContactDetail> Contacts { get; set; } = [];
}
public sealed class ContactDetail
{
    public string Label { get; set; } = "";
    public string Value { get; set; } = "";
}
