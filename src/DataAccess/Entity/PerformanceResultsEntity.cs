namespace DataAccess.Entity
{
    public class PerformanceResultsEntity
    {
        public string Division { get; set; } = null!;
        public int CurrentPlace { get; set; }
        public CompetitorEntity[] Competitors { get; set; } = null!;
        public PoleDanceResultEntity Result { get; set; } = null!;
    }
}
