using System;
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

/// <summary>
/// Hub for streaming scoreboard related changes
/// </summary>
public class ScoreboardHub : Hub
{
    private readonly ILogger<ScoreboardHub> _logger;
    private readonly IScoreboardService _scoreboardService;
    private readonly ICompetitionDataAccess _competitionDataAccess;
    private readonly ICompetitionStatusService _competitionStatusService;

    public ScoreboardHub(ILogger<ScoreboardHub> logger, IScoreboardService scoreboardService, ICompetitionDataAccess competitionDataAccess, ICompetitionStatusService competitionStatusService)
    {
        _logger = logger;
        _scoreboardService = scoreboardService;
        _competitionDataAccess = competitionDataAccess;
        _competitionStatusService = competitionStatusService;
    }

    public ChannelReader<ScoreboardStatusModel> StreamScoreboardStatus()
    {
        _logger.LogInformation("Stream connection for scoreboad status received");

        return ObservableEx.CombineLatest(
            _scoreboardService.GetScoreboardModeObservable(),
            _scoreboardService.GetActiveResultsObservable(),
            _scoreboardService.GetActiveDivisionObservable(),
            _competitionStatusService.GetLatestUpdateTime()
        ).Select(CreateScoreboardStatusModel)
        .AsChannelReader();
    }

    private ScoreboardStatusModel CreateScoreboardStatusModel((ScoreboardMode scoreboardMode, (DivisionEntity, CompetitionOrderEntity)? result, string? divisionName, DateTime latestUpdate) combined)
    {
        var upcoming = combined.divisionName != null ? _competitionDataAccess.GetDivisionEntity(combined.divisionName)?.CompetitionOrder.ToUpcomingCompetitorModelArray() ?? Array.Empty<UpcomingCompetitorModel>() : Array.Empty<UpcomingCompetitorModel>();
        return new ScoreboardStatusModel
        {
            LatestUpdate = combined.latestUpdate,
            ScoreboardMode = combined.scoreboardMode.ToScoreboardModeModel(),
            Result = combined.result.HasValue ? CreatePerformanceResultsContentModel(combined.result.Value) : null,
            UpcomingCompetitors = upcoming,
        };
    }

    private static PerformanceResultsContentModel CreatePerformanceResultsContentModel((DivisionEntity, CompetitionOrderEntity) resultEntity)
    {
        return new PerformanceResultsContentModel
        {
            Competitors = resultEntity.Item2.Competitors.Select(EntityMappingExtensions.ToCompetitorModel).ToArray(),
            Division = resultEntity.Item1.Name,
            CurrentPlace = CompetitionOrderUtil.CalculatePlacement(resultEntity.Item1.CompetitionOrder, resultEntity.Item2.Id),
            Result = resultEntity.Item2.Result?.ToPoleSportResultModel() ?? new PoleSportResultModel()
        };
    }
}
