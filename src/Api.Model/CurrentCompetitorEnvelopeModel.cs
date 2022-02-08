namespace Api.Models
{
    /// <summary>
    /// Envelope for current competitor
    /// </summary>
    public class CurrentCompetitorEnvelopeModel : BaseEnvelopeModel<CurrentCompetitorContentModel>
    {
        public CurrentCompetitorEnvelopeModel()
        {
            Version = "1";
            Type = "current-competitor";
        }
    }
}
