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

    public ScoreboardHub(ILogger<ScoreboardHub> logger, IScoreboardService scoreboardService)
    {
        _logger = logger;
        _scoreboardService = scoreboardService;
    }

    public ChannelReader<ScoreboardStatusModel> StreamScoreboardStatus()
    {
        _logger.LogInformation("Stream connection for scoreboad status received");

        return ObservableEx.CombineLatest(
            _scoreboardService.GetScoreboardModeObservable(),
            _scoreboardService.GetActiveResultsObservable(),
            _scoreboardService.GetActiveDivisionObservable()
        ).Select(CreateScoreboardStatusModel)
        .AsChannelReader();
    }

    private ScoreboardStatusModel CreateScoreboardStatusModel((ScoreboardMode scoreboardMode, (DivisionEntity, CompetitionOrderEntity)? result, DivisionEntity? division) combined)
    {
        return new ScoreboardStatusModel
        {
            ScoreboardMode = combined.scoreboardMode.ToScoreboardModeModel(),
            Result = combined.result.HasValue ? CreatePerformanceResultsContentModel(combined.result.Value) : null,
            UpcomingCompetitors = combined.division?.CompetitionOrder.ToUpcomingCompetitorModelArray() ?? Array.Empty<UpcomingCompetitorModel>()
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
