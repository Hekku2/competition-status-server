using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Represents file model of current competitor performing or performing next
    /// when no other competitor is not active.
    /// </summary>
    public class CurrentCompetitorFileModel
    {
        /// <summary>
        /// ID of competitor. This might not be set if competitor is not listed.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Division of competitor(s). Example: "Senior Women"
        /// </summary>
        [Required]
        public string Division { get; set; } = null!;

        /// <summary>
        /// Competitor(s)
        /// </summary>
        [Required]
        public CompetitorFileModel[] Competitors { get; set; } = null!;
    }
}
