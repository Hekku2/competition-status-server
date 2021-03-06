using System.ComponentModel.DataAnnotations;

namespace Api.Models;

/// <summary>
/// Describes a result for competitor(s) in for a single performance and what
/// place it did achieve, if any.
/// </summary>
public class PerformanceResultsContentModel
{
    /// <summary>
    /// Name of the division. Should match some division in currenlty active
    /// competition.
    /// </summary>
    /// <example>Senior Women</example>
    /// <example>Mixed doubles</example>
    [Required]
    public string Division { get; set; } = null!;

    /// <summary>
    /// Placement that the competitor(s) received with this result. This is null
    /// If current place couldn't be calculated.
    /// Starts from 1.
    /// </summary>
    /// <example>1</example>
    /// <example>13</example>
    public int? CurrentPlace { get; set; }

    /// <summary>
    /// Competitors that did the performance. This should contain at least one
    /// item.
    /// </summary>
    [Required]
    public CompetitorModel[] Competitors { get; set; } = null!;

    /// <summary>
    /// Result/score of the performance.
    /// </summary>
    [Required]
    public PoleSportResultModel Result { get; set; } = null!;
}
