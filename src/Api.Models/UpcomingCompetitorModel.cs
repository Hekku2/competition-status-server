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
        /// <example>123</example>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Competitor(s). Contains at least one entity.
        /// </summary>
        [Required]
        public CompetitorModel[] Competitors { get; set; } = null!;
    }
}
