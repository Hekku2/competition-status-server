using System.ComponentModel.DataAnnotations;

namespace Api.Models;

/// <summary>
/// Represents a participation in division by one or multiple competitors.
/// </summary>
public class ParticipationModel
{
    /// <summary>
    /// Division name. Should match some division in currently active
    /// competition
    /// </summary>
    /// <example>Senior Women</example>
    /// <example>Mixed doubles</example>
    [Required]
    public string Division { get; set; } = null!;

    /// <summary>
    /// Unique ID for these comeptitors
    /// </summary>
    /// <example>123</example>
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
    /// If true, competitors are shown as forfeited for this division.
    /// 
    /// In this context, forfeit can happen if:
    /// a) Competitor doesn't show up for competition
    /// b) Competitor gets injured and is unable to continue
    /// c) Competitor is disqualified
    /// 
    /// This doesn't care if it's competitor's fault or not, this just
    /// means that competitors score is not shown.
    /// 
    /// This means following:
    /// a) Results are not shown, if given.
    /// b) These competitors are not shown schedule.
    /// c) When listed, competitors are shown in the bottom part of the
    /// listing.
    /// </summary>
    /// <example>true</example>
    /// <example>false</example>
    [Required]
    public bool Forfeit { get; set; }
}
