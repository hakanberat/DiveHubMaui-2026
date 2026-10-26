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
    public string Experience { get; set; } = "";
    public string Access { get; set; } = "";
    public string MinimumAge { get; set; } = "";
    public string Prerequisites { get; set; } = "";
    public string DiveCount { get; set; } = "";
    public string Source { get; set; } = "";
    public string SourceNote { get; set; } = "";
    [JsonIgnore] public string DepthLabel => MaxDepth is {} depth ? $"{depth:0.#} m" : "";
    [JsonIgnore] public string Eyebrow => IsSite
        ? string.Join(" · ", new[] { Access.ToUpperInvariant(), DepthLabel }.Where(value => !string.IsNullOrWhiteSpace(value)))
        : Category.ToUpperInvariant();
    [JsonIgnore] public bool IsSite => Kind == "site";
    [JsonIgnore] public bool IsCourse => Kind == "course";
    [JsonIgnore] public bool HasEyebrow => !string.IsNullOrWhiteSpace(Eyebrow);
    [JsonIgnore] public bool HasSummary => !string.IsNullOrWhiteSpace(Summary);
    [JsonIgnore] public bool HasDescription => !string.IsNullOrWhiteSpace(Description);
    [JsonIgnore] public bool HasDepth => IsSite && MaxDepth.HasValue;
    [JsonIgnore] public bool HasAccess => IsSite && !string.IsNullOrWhiteSpace(Access);
    [JsonIgnore] public bool HasExperience => IsSite && !string.IsNullOrWhiteSpace(Experience);
    [JsonIgnore] public bool HasSiteInfo => HasDepth || HasAccess || HasExperience;
    [JsonIgnore] public bool HasMinimumAge => IsCourse && !string.IsNullOrWhiteSpace(MinimumAge);
    [JsonIgnore] public bool HasPrerequisites => IsCourse && !string.IsNullOrWhiteSpace(Prerequisites);
    [JsonIgnore] public bool HasDiveCount => IsCourse && !string.IsNullOrWhiteSpace(DiveCount);
    [JsonIgnore] public bool HasCourseInfo => HasMinimumAge || HasPrerequisites || HasDiveCount;
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
