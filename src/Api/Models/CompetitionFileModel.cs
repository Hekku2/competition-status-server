using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CompetitionFileModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DivisionFileModel[] Divisions { get; set; } = null!;
    }

    public class DivisionFileModel
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public CompetitorPositionFileModel[] Order { get; set; } = null!;
    }

    public class CompetitorPositionFileModel
    {
        [Required]
        public CompetitorFileModel[] Competitors { get; set; } = null!;

        [Required]
        public bool Forfeit { get; set; }

        public PoleResultFileModel? Results { get; set; }
    }

    public class CompetitorFileModel
    {
        [Required]
        public string Name { get; set; } = null!;
        public string? Team { get; set; }
    }

    public class PoleResultFileModel
    {
        public decimal ArtisticScore { get; set; }
        public decimal ExecutionScore { get; set; }
        public decimal DifficultyScore { get; set; }
        public decimal HeadJudgePenalty { get; set; }
    }
}
