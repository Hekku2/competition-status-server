using System.ComponentModel.DataAnnotations;

namespace Api.Models;

/// <summary>
/// Describes division that is saved in file. User for JSON conversions.
/// </summary>
public class DivisionFileModel
{
    /// <summary>
    /// Name of the division.
    /// </summary>
    /// <example>Senior Women</example>
    /// <example>Mixed doubles</example>
    [Required]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Competitors participating in this division.
    /// </summary>
    [Required]
    public CompetitorPositionFileModel[] Items { get; set; } = null!;
}
