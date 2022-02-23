using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class ScoreboardStatusModel
{
    [Required]
    public ScoreboardModeModel ScoreboardMode { get; set; }

    public PerformanceResultsContentModel? Result { get; set; }

    [Required]
    public UpcomingCompetitorModel[] UpcomingCompetitors { get; set; } = Array.Empty<UpcomingCompetitorModel>();
}
