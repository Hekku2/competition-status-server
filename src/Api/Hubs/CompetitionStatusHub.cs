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
        _logger.LogInformation("Stream connection received");
        return _competitionStatusService.GetCurrentCompetitorObservable().Select(GetCurrentCompetitor).AsChannelReader();
    }

    private CurrentCompetitorEnvelopeModel GetCurrentCompetitor(CurrentCompetitorsEntity? entity)
    {
        return new CurrentCompetitorEnvelopeModel
        {
            Content = entity != null ? CreateCurrentCompetitorContentModel(entity) : null
        };
    }

    private static CurrentCompetitorContentModel CreateCurrentCompetitorContentModel(CurrentCompetitorsEntity entity)
    {
        return new CurrentCompetitorContentModel
        {
            Division = entity.Division,
            Competitors = entity.Competitors.Select(CreateCompetitorModel).ToArray()
        };
    }

    private static CompetitorModel CreateCompetitorModel(CompetitorEntity entity)
    {
        return new CompetitorModel
        {
            Name = entity.Name,
            Team = entity.Team
        };
    }
}
