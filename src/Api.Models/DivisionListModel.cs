using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class DivisionListModel
{
    /// <summary>
    /// Name of the division.
    /// </summary>
    /// <example>Senior Women</example>
    /// <example>Mixed doubles</example>
    [Required]
    public string Name { get; set; } = null!;
}
