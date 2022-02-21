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
        return _scoreboardService.GetScoreboardModeObservable().Select(CreateScoreboardStatusModel).AsChannelReader();
    }

    private ScoreboardStatusModel CreateScoreboardStatusModel(ScoreboardMode entity)
    {
        return new ScoreboardStatusModel
        {
            ScoreboardMode = entity.ToScoreboardModeModel()
        };
    }
}
