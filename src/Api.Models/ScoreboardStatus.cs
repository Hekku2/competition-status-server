using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class ScoreboardStatusModel
{
    [Required]
    public DateTime LatestUpdate { get; set; }

    [Required]
    public ScoreboardModeModel ScoreboardMode { get; set; }

    public PerformanceResultsContentModel? Result { get; set; }

    public string? Division { get; set; }

    [Required]
    public UpcomingCompetitorModel[] UpcomingCompetitors { get; set; } = Array.Empty<UpcomingCompetitorModel>();

    [Required]
    public ParticipationRowModel[] Results { get; set; } = Array.Empty<ParticipationRowModel>();
}
