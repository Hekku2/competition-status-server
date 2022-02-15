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
        [Required]
        public string EventName { get; set; } = null!;

        /// <summary>
        /// Timestamp indicating when this status was generated
        /// </summary>
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
