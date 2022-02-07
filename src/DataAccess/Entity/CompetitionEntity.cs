namespace DataAccess.Entity
{
    /// <summary>
    /// This class represents the whole competition
    /// </summary>
    public class CompetitionEntity
    {
        public string Name { get; set; } = null!;
        public CurrentCompetitorsEntity? CurrentCompetitor { get; set; }
        public DivisionEntity[] Divisions { get; set; } = null!;
    }
}
