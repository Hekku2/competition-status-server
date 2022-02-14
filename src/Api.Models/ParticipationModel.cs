using System.ComponentModel.DataAnnotations;

namespace Api.Models;

/// <summary>
/// Represents a participation in division by one or multiple competitors.
/// </summary>
public class ParticipationModel
{
    /// <summary>
    /// Division name
    /// </summary>
    [Required]
    public string Division { get; set; } = null!;

    /// <summary>
    /// Unique ID for these comeptitors
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Competitors
    /// </summary>
    [Required]
    public CompetitorModel[] Competitors { get; set; } = null!;

    /// <summary>
    /// Result for the competitors. This can be missing if competitor
    /// has not yet received it, or if competitor has forfeited.
    /// </summary>
    public PoleSportResultModel? Result { get; set; }

    /// <summary>
    /// If true, this competitor has forfeited and should not have a result
    /// </summary>
    [Required]
    public bool Forfeit { get; set; }
}
