using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Represents a single competitor. May be a part of team.
    /// </summary>
    public class CompetitorFileModel
    {
        /// <summary>
        /// Name of the competitor
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Team, if given.
        /// </summary>
        public string? Team { get; set; }
    }
}
