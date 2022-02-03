using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class CurrentCompetitorContentModel
    {
        [Required]
        public string Division { get; set; } = null!;
        public string? Category { get; set; }
        public string? Target { get; set; }
        public string? Attempt { get; set; }
        public int? CurrentPlace { get; set; }
        [Required]
        public CompetitorModel[] Competitors { get; set; } = null!;
        [Required]
        public object[] PreviousResults { get; set; } = null!;
    }
}
