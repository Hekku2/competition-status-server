using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AcceptanceTests.Util;
using Api.Hubs;
using Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers.Json;

namespace AcceptanceTests;

public class ScoreboardTests
{
    private RestClient _client;

    private ScoreboardStatusModel _latestMessage;
    private CancellationTokenSource _source;

    private const string ApiName = "Scoreboard";
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
            .Build();

        await connection.StartAsync(_source.Token);
        var channel = await connection.StreamAsChannelAsync<ScoreboardStatusModel>(nameof(ScoreboardHub.StreamScoreboardStatus));
        _ = channel.ReadUntilStopped((item) =>
        {
            _latestMessage = item;
        }, _source.Token);

        _client = new RestClient($"{uri}{ApiName}");
        _client.UseSystemTextJson(new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        });
    }

    [Test]
    public async Task ScoreboardModeChanges()
    {
        var response = await _client.GetJsonAsync<ScoreboardStatusModel>("", _source.Token);

        response.ScoreboardMode.Should().Be(ScoreboardModeModel.Unknown, "Initial status should be unknown");
    }
}
