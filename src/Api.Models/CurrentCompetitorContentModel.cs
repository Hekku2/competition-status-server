using System.ComponentModel.DataAnnotations;

namespace Api.Models;

/// <summary>
/// Represents current competitor who is performing or performing next
/// when no other competitor is not active.
/// </summary>
public class CurrentCompetitorContentModel
{
    /// <summary>
    /// Division of competitor(s). This should match some division in active
    /// competition
    /// </summary>
    /// <example>Senior Women</example>
    [Required]
    public string Division { get; set; } = null!;

    /// <summary>
    /// Competitor(s). This should have at least one value, but may have
    /// multiple values if there are multiple persons performing for single
    /// performance.
    /// </summary>
    [Required]
    public CompetitorModel[] Competitors { get; set; } = null!;
}
