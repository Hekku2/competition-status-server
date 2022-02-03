namespace Api.Models
{
    public class CompetitionStatusEnvelopeModel : BaseEnvelopeModel<CompetitionStatusContentModel>
    {
        public CompetitionStatusEnvelopeModel()
        {
            Version = "1";
            Type = "competition-status";
        }
    }
}
