using System.Linq;
using System.Reactive.Linq;
using System.Threading.Channels;
using Api.Models;
using Api.Services.Interfaces;
using Api.Util;
using DataAccess.Entity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Api.Hubs;

public class CompetitionStatusHub : Hub
{
    private readonly ILogger<CompetitionStatusHub> _logger;
    private readonly ICompetitionStatusService _competitionStatusService;

    public CompetitionStatusHub(ILogger<CompetitionStatusHub> logger, ICompetitionStatusService competitionStatusService)
    {
        _logger = logger;
        _competitionStatusService = competitionStatusService;
    }

    public ChannelReader<CurrentCompetitorEnvelopeModel> StreamCompetitors()
    {
        _logger.LogInformation("Stream connection for current competitor received");
        return _competitionStatusService.GetCurrentCompetitorObservable().Select(GetCurrentCompetitor).AsChannelReader();
    }

    public ChannelReader<PerformanceResultsEnvelopeModel> StreamPerformanceResults()
    {
        _logger.LogInformation("Stream connection for performance results received");
        return _competitionStatusService.GetPerformanceResultsObservable().Select(CreatePerformanceResultsEnvelopeModel).AsChannelReader();
    }

    private PerformanceResultsEnvelopeModel CreatePerformanceResultsEnvelopeModel(PerformanceResultsEntity entity)
    {
        return new PerformanceResultsEnvelopeModel
        {
            Content = entity.ToPerformanceResultsContentModel()
        };
    }

    private CurrentCompetitorEnvelopeModel GetCurrentCompetitor(CurrentCompetitorsEntity? entity)
    {
        return new CurrentCompetitorEnvelopeModel
        {
            Content = entity?.ToCurrentCompetitorContentModel()
        };
    }
}
