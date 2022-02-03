namespace DataAccess.Entity
{
    /// <summary>
    /// This class represents a single division
    /// </summary>
    public class DivisionEntity
    {
        /// <summary>
        /// Name of this division
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Competitors in order, zero index is first. Forfeited competitors should be shown last.
        /// </summary>
        public CompetitionOrderEntity[] CompetitionOrder { get; set; } = null!;
    }
}
