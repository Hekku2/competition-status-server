using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// This represens a current status for a division.
    /// This is generated on the fly.
    /// </summary>
    public class DivisionStatusModel
    {
        /// <summary>
        /// Name if the division. Example: "Senior women"
        /// </summary>
        /// <example>Senior women</example>
        /// <example>Mixed doubles</example>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Current results in order. Forfeited are not yet listed
        /// </summary>
        [Required]
        public ParticipationRowModel[] Results { get; set; } = null!;

        /// <summary>
        /// Forfeited competitors. Is empty if no one has forfeited. These are
        /// not returned in any special order.
        /// </summary>
        [Required]
        public ParticipationRowModel[] Forfeited { get; set; } = null!;

        /// <summary>
        /// Upcoming competitors. First is in zero index.
        /// This can be empty, if no competitors are remaining.
        /// </summary>
        [Required]
        public UpcomingCompetitorModel[] UpcomingCompetitorModels { get; set; } = null!;
    }
}
