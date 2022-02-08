using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Represents current competitor who is performing or performing next
    /// when no other competitor is not active.
    /// </summary>
    public class CurrentCompetitorContentModel
    {
        /// <summary>
        /// Division of competitor(s). Example: "Senior Women"
        /// </summary>
        [Required]
        public string Division { get; set; } = null!;

        /// <summary>
        /// Competitor(s)
        /// </summary>
        [Required]
        public CompetitorModel[] Competitors { get; set; } = null!;
    }
}
