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

public class ScoreboardTests
{
    private ScoreboardApi _client;
    private ScoreboardStatusModel _latestMessage;
    private CancellationTokenSource _source;

    private const string HubName = "scoreboard-hub";

    [SetUp]
    public async Task Setup()
    {
        var uri = Environment.GetEnvironmentVariable("APP_URI");
        if (string.IsNullOrWhiteSpace(uri))
        {
            Assert.Inconclusive("URI not set. Unable to execute acceptance tests.");
        }
        _latestMessage = null;
        _source = new CancellationTokenSource();

        var connection = new HubConnectionBuilder()
            .WithUrl($"{uri}{HubName}")
            .AddJsonProtocol(options =>
            {
                var enumConverter = new JsonStringEnumConverter();
                options.PayloadSerializerOptions.Converters.Add(enumConverter);
            })
            .Build();

        await connection.StartAsync(_source.Token);
        var channel = await connection.StreamAsChannelAsync<ScoreboardStatusModel>("StreamScoreboardStatus", _source.Token);
        _ = channel.ReadUntilStopped((item) =>
        {
            _latestMessage = item;
        }, _source.Token);

        _client = new ScoreboardApi(uri);
    }

    [Test]
    public async Task ScoreboardModeChanges()
    {
        await ChangeAndVerify(ScoreboardModeModel.CompetitorResults);
        await ChangeAndVerify(ScoreboardModeModel.DivisionStatus);
        await ChangeAndVerify(ScoreboardModeModel.UpcomingCompetitors);
        await ChangeAndVerify(ScoreboardModeModel.Unknown);
    }

    private async Task ChangeAndVerify(ScoreboardModeModel model)
    {
        await _client.ScoreboardSetScoreboardModeAsync(model);
        var newStatus = await _client.ScoreboardGetStatusAsync(_source.Token);
        newStatus.ScoreboardMode.Should().Be(model);
        _latestMessage.ScoreboardMode.Should().Be(model);
    }
}
