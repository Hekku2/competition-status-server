namespace DataAccess.Entity
{
    public class CurrentCompetitorsEntity
    {
        public int? Id { get; set; }
        public string Division { get; set; } = null!;
        public CompetitorEntity[] Competitors { get; set; } = null!;
    }
}
