using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Represents a single competitor in file. May be a part of team.
    /// </summary>
    public class CompetitorFileModel
    {
        /// <summary>
        /// Name of the competitor
        /// </summary>
        /// <example>Matt Smith</example>
        /// <example>Sunry Won Pickelson</example>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Team, if given.
        /// </summary>
        /// <example>Team Pole Queens</example>
        public string? Team { get; set; }
    }
}
