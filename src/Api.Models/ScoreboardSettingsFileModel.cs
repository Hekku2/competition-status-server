namespace Api.Models
{
    public class ScoreboardSettingsFileModel
    {
        public ScoreboardMode ScoreboardMode { get; set; }

        /// <summary>
        /// Name of the currently selected division
        /// </summary>
        /// <example>Senior women</example>
        public string? ActiveDivision { get; set; }

        public int? ActiveResult { get; set; }
    }
}
