using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class CompetitorResultModel
{
    /// <summary>
    /// ID of competitor whose results are set
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Results to be set. If null, results are removed
    /// </summary>
    public PoleResultFileModel? Results { get; set; }
}
