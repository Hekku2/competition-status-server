using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    /// <summary>
    /// Describes the filemodel that is used to save current status of
    /// competition. This model should hold all information of the competition
    /// that can be saved to file.
    /// </summary>
    public class CompetitionFileModel
    {
        /// <summary>
        /// Name of the whole competition.
        /// </summary>
        /// <example>National finals 2022</example>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Divisions for this competition.
        /// </summary>
        [Required]
        public DivisionFileModel[] Divisions { get; set; } = null!;

        /// <summary>
        /// Current competitor
        /// </summary>
        public CurrentCompetitorFileModel? CurrentCompetitor { get; set; }
    }
}
