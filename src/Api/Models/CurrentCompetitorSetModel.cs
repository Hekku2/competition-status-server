namespace Api.Models
{
    /// <summary>
    /// Model used to set ID of current competitor(s)
    /// </summary>
    public class CurrentCompetitorSetModel
    {
        /// <summary>
        /// ID of currently active competitor (or who is performing next if no
        /// one is active).
        /// 
        /// If null is used, active competitor is cleared.
        /// </summary>
        public int? Id { get; set; }
    }
}
