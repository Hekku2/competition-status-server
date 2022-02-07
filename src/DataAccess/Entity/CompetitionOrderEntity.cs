namespace DataAccess.Entity
{
    /// <summary>
    /// Representes person / persons participating
    /// </summary>
    public class CompetitionOrderEntity
    {
        public CompetitorEntity[] Competitors { get; set; } = null!;

        /// <summary>
        /// If true, competitor should be shown as forfeit
        /// </summary>
        public bool Forfeit { get; set; }

        public PoleDanceResultEntity? Result { get; set; }
    }
}