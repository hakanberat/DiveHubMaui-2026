using System.Globalization;
using SQLite;
namespace DiveHubMauiApp.Models;
[Table("Dives"), SQLite.Preserve(AllMembers=true)]
public sealed class DiveRecord
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Unique] public int DiveNumber { get; set; }
    [NotNull, MaxLength(160)] public string DiveSite { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Today;
    public double MaxDepth { get; set; }
    public int Duration { get; set; }
    public int StartingPressure { get; set; }
    public int EndingPressure { get; set; }
    [MaxLength(120)] public string ExposureProtection { get; set; } = "";
    public double Weight { get; set; }
    [MaxLength(120)] public string DiveLeader { get; set; } = "";
    [MaxLength(120)] public string DiveBuddy { get; set; } = "";
    [MaxLength(6000)] public string Notes { get; set; } = "";
    [Ignore] public string NumberLabel => $"DIVE {DiveNumber:000}";
    [Ignore] public string DateLabel => Date.ToString("dd MMM yyyy", CultureInfo.GetCultureInfo("en-US"));
    [Ignore] public string MetricsLabel => $"{MaxDepth:0.#} m  ·  {Duration} min";
}
public static class DiveValidation
{
    public static void Validate(DiveRecord dive)
    {
        if (dive.Id < 0) throw new ArgumentException("Invalid record identifier.");
        if (dive.DiveNumber < 1 || dive.DiveNumber > 999999) throw new ArgumentException("Dive number must be between 1 and 999,999.");
        if (string.IsNullOrWhiteSpace(dive.DiveSite) || dive.DiveSite.Length > 160) throw new ArgumentException("Enter a dive site (up to 160 characters).");
        if (dive.Date.Date > DateTime.Today || dive.Date.Date < new DateTime(1900, 1, 1)) throw new ArgumentException("Choose a date from 1900 through today.");
        if (!double.IsFinite(dive.MaxDepth) || dive.MaxDepth <= 0 || dive.MaxDepth > 400) throw new ArgumentException("Depth must be greater than 0 and at most 400 m.");
        if (dive.Duration < 1 || dive.Duration > 1440) throw new ArgumentException("Duration must be between 1 and 1,440 minutes.");
        if (dive.StartingPressure < 1 || dive.StartingPressure > 400 || dive.EndingPressure < 0 || dive.EndingPressure > dive.StartingPressure) throw new ArgumentException("Enter starting pressure from 1–400 bar and ending pressure from 0 through starting pressure.");
        if (!double.IsFinite(dive.Weight) || dive.Weight < 0 || dive.Weight > 100) throw new ArgumentException("Weight must be between 0 and 100 kg.");
        if (dive.ExposureProtection.Length > 120 || dive.DiveLeader.Length > 120 || dive.DiveBuddy.Length > 120) throw new ArgumentException("Exposure protection, leader and buddy are limited to 120 characters each.");
        if (dive.Notes.Length > 6000) throw new ArgumentException("Notes are limited to 6,000 characters.");
    }
}
