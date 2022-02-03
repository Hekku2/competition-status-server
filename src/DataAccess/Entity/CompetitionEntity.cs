namespace DataAccess.Entity
{
    /// <summary>
    /// This class represents the whole competition
    /// </summary>
    public class CompetitionEntity
    {
        public string Name { get; set; } = null!;
        public DivisionEntity[] Divisions { get; set; } = null!;
    }
}
