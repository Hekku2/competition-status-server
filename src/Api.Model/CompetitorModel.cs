namespace Api.Models
{
    /// <summary>
    /// Represents a single competitor.
    /// </summary>
    public class CompetitorModel
    {
        /// <summary>
        /// Name of competitor
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Team of competitor
        /// </summary>
        public string? Team { get; set; }
    }
}
