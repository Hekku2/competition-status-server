using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Current status of the competition.
    /// </summary>
    public class CompetitionStatusContentModel
    {
        /// <summary>
        /// Name of the event. Example: National finals 2022
        /// </summary>
        /// <example>National finals 2022</example>
        [Required]
        public string EventName { get; set; } = null!;

        /// <summary>
        /// Timestamp indicating when this status was generated.
        /// This is always In UTC
        /// Format "yyyy-MM-ddTHH:mm:ss.fffZ"
        /// </summary>
        /// <example>2022-02-15T19:14:25.004Z</example>
        [Required]
        public string CreatedAt { get; set; } = null!;

        /// <summary>
        /// Current status of divisions
        /// </summary>
        [Required]
        public DivisionStatusModel[] Divisions { get; set; } = null!;

        /// <summary>
        /// Current competitor. May be null if not set.
        /// </summary>
        public CurrentCompetitorContentModel? CurrentCompetitor { get; set; }
    }
}
