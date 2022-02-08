using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// This represents a finished or or otherwise resolved performance result.
    /// Result might be missing, if performance has been finished but has not been
    /// graded yet or if the competitor has forfeited.
    /// </summary>
    public class ResultRowModel
    {
        /// <summary>
        /// Unique ID for these comeptitors
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Competitors
        /// </summary>
        [Required]
        public CompetitorModel[] Competitors { get; set; } = null!;

        /// <summary>
        /// Result for the competitors. This can be missing if competitor
        /// has not yet received it, or if competitor has forfeited.
        /// </summary>
        public PoleSportResultModel? Result { get; set; }

        /// <summary>
        /// If true, this competitor has forfeited and should not have a result
        /// </summary>
        [Required]
        public bool Forfeit { get; set; }
    }
}
