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
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Current results in order.
        /// </summary>
        [Required]
        public ResultRowModel[] Results { get; set; } = null!;


        /// <summary>
        /// Upcoming competitors. First is in zero index.
        /// This can be empty, if no competitors are remaining.
        /// </summary>
        [Required]
        public UpcomingCompetitorModel[] UpcomingCompetitorModels { get; set; } = null!;
    }
}
