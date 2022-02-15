namespace Api.Models
{
    /// <summary>
    /// Contains current status of competition, of competition is active.
    /// If competition is not active, Content can be null.
    /// </summary>
    public class CompetitionStatusEnvelopeModel : BaseEnvelopeModel<CompetitionStatusContentModel>
    {
        public CompetitionStatusEnvelopeModel()
        {
            Version = "1";
            Type = "competition-status";
        }
    }
}
