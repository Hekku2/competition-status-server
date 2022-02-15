namespace Api.Models;

/// <summary>
/// Represents a single competitor.
/// </summary>
public class CompetitorModel
{
    /// <summary>
    /// Name of competitor
    /// </summary>
    /// <example>Matt Smith</example>
    /// <example>Sunry Won Pickelson</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Team of competitor
    /// </summary>
    /// <example>Team Pole Queens</example>
    public string? Team { get; set; }
}
