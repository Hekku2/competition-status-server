namespace DataAccess.Entity
{
    public class CurrentCompetitorsEntity
    {
        public int Id { get; set; }
        public string Division { get; set; } = null!;
        public string? Category { get; set; }
        public string? Target { get; set; }
        public string? Attempt { get; set; }
        public int? CurrentPlace { get; set; }
        public CompetitorEntity[] Competitors { get; set; } = null!;
        public object[] PreviousResults { get; set; } = null!;
    }
}
