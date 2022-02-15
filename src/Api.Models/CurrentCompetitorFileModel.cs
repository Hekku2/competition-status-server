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
        /// <example>25</example>
        public int? Id { get; set; }

        /// <summary>
        /// Division of competitor(s). This should match some division in
        /// current competition.
        /// </summary>
        /// <example>Senior Women</example>
        /// <example>Masters +40</example>
        [Required]
        public string Division { get; set; } = null!;

        /// <summary>
        /// Competitor(s). This should have at least one value, but may have
        /// multiple values if there are multiple persons performing for single
        /// performance.
        /// </summary>
        [Required]
        public CompetitorFileModel[] Competitors { get; set; } = null!;
    }
}
