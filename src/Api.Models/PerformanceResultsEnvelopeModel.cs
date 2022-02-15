namespace Api.Models;

/// <summary>
/// Message containing performance results
/// </summary>
public class PerformanceResultsEnvelopeModel : BaseEnvelopeModel<PerformanceResultsContentModel>
{
    public PerformanceResultsEnvelopeModel()
    {
        Version = "1";
        Type = "performance-results";
    }
}
