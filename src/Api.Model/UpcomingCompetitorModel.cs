using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Represents an upcoming competitor.
    /// </summary>
    public class UpcomingCompetitorModel
    {
        /// <summary>
        /// Unique ID for these competitors
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Competitor(s)
        /// </summary>
        [Required]
        public CompetitorModel[] Competitors { get; set; } = null!;
    }
}
