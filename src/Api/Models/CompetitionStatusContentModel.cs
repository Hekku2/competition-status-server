using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CompetitionStatusContentModel
    {
        [Required]
        public string EventName { get; set; } = null!;

        [Required]
        public string CreatedAt { get; set; } = null!;

        [Required]
        public DivisionStatusModel[] Divisions { get; set; } = null!;
    }

    public class DivisionStatusModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public ResultRowModel[] Results { get; set; } = null!;
    }

    public class ResultRowModel
    {
        [Required]
        public CompetitorModel[] Competitors { get; set; } = null!;

        [Required]
        public PoleSportResultModel Result { get; set; } = null!;
    }

    public class PoleSportResultModel
    {
        [Required]
        public decimal Total { get; set; }

        [Required]
        public decimal ArtisticScore { get; set; }

        [Required]
        public decimal ExecutionScore { get; set; }

        [Required]
        public decimal DifficultyScore { get; set; }

        [Required]
        public decimal HeadJudgePenalty { get; set; }
    }
}
