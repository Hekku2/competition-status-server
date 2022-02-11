using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class PerformanceResultsContentModel
{
    [Required]
    public string Division { get; set; } = null!;
    [Required]
    public int CurrentPlace { get; set; }
    [Required]
    public CompetitorModel[] Competitors { get; set; } = null!;
    [Required]
    public PoleSportResultModel Result { get; set; } = null!;
}
