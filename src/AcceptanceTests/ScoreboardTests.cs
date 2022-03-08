using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AcceptanceTests.Util;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Model;

namespace AcceptanceTests;

public class ScoreboardTests : AcceptanceTestBase
{
    private ScoreboardStatusModel _latestMessage;

    private const string HubName = "scoreboard-hub";

    protected override async Task OnSetup()
    {
        _latestMessage = null;

        var channel = await ScoreboardHub.StreamAsChannelAsync<ScoreboardStatusModel>("StreamScoreboardStatus", TokenSource.Token);
        _ = channel.ReadUntilStopped((item) =>
        {
            _latestMessage = item;
        }, TokenSource.Token);
    }

    [Test]
    public async Task ScoreboardModeChanges()
    {
        await ChangeAndVerify(ScoreboardModeModel.CompetitorResults);
        await ChangeAndVerify(ScoreboardModeModel.DivisionStatus);
        await ChangeAndVerify(ScoreboardModeModel.UpcomingCompetitors);
        await ChangeAndVerify(ScoreboardModeModel.Unknown);
    }

    [Test]
    public async Task SetResultsUpdatesScoreboard()
    {
        var competitorId = 6;

        var model = TestUtil.CreateDataModel();
        await CompetitionApi.CompetitionUploadCompetitionAsync(model, TokenSource.Token);
        await ScoreboardApi.ScoreboardSelectResultForShowingAsync(competitorId, TokenSource.Token);
        await Task.Delay(TimeSpan.FromSeconds(2));
        var updateTime = _latestMessage.LatestUpdate;

        var resultModel = new CompetitorResultModel
        {
            Id = competitorId,
            Results = new PoleResultFileModel(0, 0, 0, 0)
            {
                ArtisticScore = 1,
                DifficultyScore = 2,
                ExecutionScore = 3,
                HeadJudgePenalty = 0
            }
        };
        await CompetitionApi.CompetitionSetResultAsync(resultModel, TokenSource.Token);

        await Task.Delay(TimeSpan.FromSeconds(2));
        _latestMessage.LatestUpdate.Should().BeAfter(updateTime);
        _latestMessage.Result.Should().NotBeNull();
        _latestMessage.Result.Result.ArtisticScore.Should().Be(resultModel.Results.ArtisticScore);
        _latestMessage.Result.Result.DifficultyScore.Should().Be(resultModel.Results.DifficultyScore);
        _latestMessage.Result.Result.ExecutionScore.Should().Be(resultModel.Results.ExecutionScore);
        _latestMessage.Result.Result.HeadJudgePenalty.Should().Be(resultModel.Results.HeadJudgePenalty);
    }

    private async Task ChangeAndVerify(ScoreboardModeModel model)
    {
        await ScoreboardApi.ScoreboardSetScoreboardModeAsync(model);
        await Task.Delay(TimeSpan.FromSeconds(2));
        var newStatus = await ScoreboardApi.ScoreboardGetStatusAsync(TokenSource.Token);
        newStatus.ScoreboardMode.Should().Be(model);
        _latestMessage.ScoreboardMode.Should().Be(model);
    }
}
