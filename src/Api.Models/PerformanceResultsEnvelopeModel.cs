namespace Api.Models;

public class PerformanceResultsEnvelopeModel : BaseEnvelopeModel<PerformanceResultsContentModel>
{
    public PerformanceResultsEnvelopeModel()
    {
        Version = "1";
        Type = "performance-results";
    }
}
