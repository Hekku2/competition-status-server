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
        /// <example>59.266</example>
        /// <example>24.000</example>
        [Required]
        public decimal ArtisticScore { get; set; }

        /// <summary>
        /// Execution score (E)
        /// </summary>
        /// <example>70.333</example>
        /// <example>65.333</example>
        [Required]
        public decimal ExecutionScore { get; set; }

        /// <summary>
        /// Difficulty score (D)
        /// </summary>
        /// <example>12.800</example>
        /// <example>9.700</example>
        [Required]
        public decimal DifficultyScore { get; set; }

        /// <summary>
        /// Head judge penalty (HJ). This is subtracted from the total.
        /// </summary>
        /// <example>0</example>
        /// <example>2.00</example>
        [Required]
        public decimal HeadJudgePenalty { get; set; }
    }
}
