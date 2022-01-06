namespace Api.Models
{
    public class CurrentCompetitorContentModel
    {
        public string Division { get; set; }
        public string? Category { get; set; }
        public string? Target { get; set; }
        public string? Attempt { get; set; }
        public int? CurrentPlace { get; set; }
        public CompetitorModel[] Competitors { get; set; }
        public object[] PreviousResults { get; set; }
    }
}
