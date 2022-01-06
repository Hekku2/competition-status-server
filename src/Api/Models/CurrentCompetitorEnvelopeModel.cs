namespace Api.Models
{
    public class CurrentCompetitorEnvelopeModel : BaseEnvelopeModel<CurrentCompetitorContentModel>
    {
        public CurrentCompetitorEnvelopeModel()
        {
            Version = "1";
            Type = "current-competitor";
        }
    }
}
