namespace DataAccess.Entity
{
    /// <summary>
    /// Representes person / persons participating
    /// </summary>
    public class CompetitionOrderEntity
    {
        /// <summary>
        /// Unique identifier for these comeptitors in this division.
        /// </summary>
        public int Id { get; set; }
        public CompetitorEntity[] Competitors { get; set; } = null!;

        /// <summary>
        /// If true, competitor should be shown as forfeit
        /// </summary>
        public bool Forfeit { get; set; }

        public PoleDanceResultEntity? Result { get; set; }
    }
}
