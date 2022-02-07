using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Represents a score in Pole Dance Sport series
    /// </summary>
    public class PoleResultFileModel
    {
        /// <summary>
        /// Artistic score (A)
        /// </summary>
        [Required]
        public decimal ArtisticScore { get; set; }

        /// <summary>
        /// Execution score (E)
        /// </summary>
        [Required]
        public decimal ExecutionScore { get; set; }

        /// <summary>
        /// Difficulty score (D)
        /// </summary>
        [Required]
        public decimal DifficultyScore { get; set; }

        /// <summary>
        /// Head judge penalty (HJ). This is subtracted from the total.
        /// </summary>
        [Required]
        public decimal HeadJudgePenalty { get; set; }
    }
}
