namespace Api.Models;

/// <summary>
/// Envelope for current competitor. Current competitor is the competitor that
/// is performing currently or is performing next, if no one is performing.
/// </summary>
public class CurrentCompetitorEnvelopeModel : BaseEnvelopeModel<CurrentCompetitorContentModel>
{
    public CurrentCompetitorEnvelopeModel()
    {
        Version = "1";
        Type = "current-competitor";
    }
}
