namespace DataAccess.Entity
{
    /// <summary>
    /// A result for single pole dance performance
    /// </summary>
    public class PoleDanceResultEntity
    {
        public decimal ArtisticScore { get; set; }
        public decimal ExecutionScore { get; set; }
        public decimal DifficultyScore { get; set; }
        public decimal HeadJudgePenalty { get; set; }
    }
}
