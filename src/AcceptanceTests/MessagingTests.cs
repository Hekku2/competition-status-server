using System;
using System.Threading;
using System.Threading.Tasks;
using AcceptanceTests.Util;
using Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using NUnit.Framework;
using RestSharp;

namespace AcceptanceTests;

public class MessagingTests
{
    private RestClient _client;

    private CurrentCompetitorEnvelopeModel _latesCurrentCompetitor;
    private PerformanceResultsEnvelopeModel _latestPerformanceResults;
    private CancellationTokenSource _source;
    private const string ApiName = "Competition";

    [SetUp]
    public async Task Setup()
    {
        var uri = Environment.GetEnvironmentVariable("APP_URI");
        if (string.IsNullOrWhiteSpace(uri))
        {
            Assert.Inconclusive("URI not set. Unable to execute acceptance tests.");
        }
        _latesCurrentCompetitor = null;

        var connection = new HubConnectionBuilder()
            .WithUrl($"{uri}competition-hub")
            .Build();

        await connection.StartAsync();
        var currentCompetitorChannel = await connection.StreamAsChannelAsync<CurrentCompetitorEnvelopeModel>("StreamCompetitors");
        var performanceResultChannel = await connection.StreamAsChannelAsync<PerformanceResultsEnvelopeModel>("StreamPerformanceResults");

        _source = new CancellationTokenSource();
        _ = currentCompetitorChannel.ReadUntilStopped((item) =>
        {
            _latesCurrentCompetitor = item;
        }, _source.Token);

        _ = performanceResultChannel.ReadUntilStopped((item) =>
        {
            _latestPerformanceResults = item;
        }, _source.Token);

        _latesCurrentCompetitor = null;
        _latestPerformanceResults = null;
        _client = new RestClient($"{uri}{ApiName}");
    }

    [TearDown]
    public void TearDown()
    {
        _source?.Cancel();
    }

    [Test]
    public async Task SettingResultHasEvent_SendsMessage()
    {
        var model = new CompetitionFileModel
        {
            Name = "New competition",
            Divisions = new[]
            {
                new DivisionFileModel
                {
                    Name = "Senior Women",
                    Items = new []
                    {
                        new CompetitorPositionFileModel
                        {
                            Id = 1,
                            Competitors = TestUtil.CreateSingleCompetitor("expected name", "my team"),
                            Results = null,
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 2,
                            Competitors = TestUtil.CreateSingleCompetitor("I should be first", "my team"),
                            Results = new PoleResultFileModel
                            {
                                ArtisticScore = 199,
                                DifficultyScore = 2,
                                ExecutionScore = 3,
                                HeadJudgePenalty = 0
                            },
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 3,
                            Competitors = TestUtil.CreateSingleCompetitor("I should be third", "my team"),
                            Results = new PoleResultFileModel
                            {
                                ArtisticScore = 0,
                                DifficultyScore = 2,
                                ExecutionScore = 3,
                                HeadJudgePenalty = 100
                            },
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 4,
                            Competitors = TestUtil.CreateSingleCompetitor("I should not be included", "my team"),
                            Results = new PoleResultFileModel
                            {
                                ArtisticScore = 199,
                                DifficultyScore = 992,
                                ExecutionScore = 3,
                                HeadJudgePenalty = 0
                            },
                            Forfeit = true
                        },
                    }
                }
            }
        };
        var result = await _client.PostJsonAsync("upload-competition", model, _source.Token);
        result.Should().Be(System.Net.HttpStatusCode.OK);

        var resultModel = new CompetitorResultModel
        {
            Id = 1,
            Results = new PoleResultFileModel
            {
                ArtisticScore = 1,
                DifficultyScore = 2,
                ExecutionScore = 3,
                HeadJudgePenalty = 0
            }
        };
        var setResuttResult = await _client.PostJsonAsync("set-result", resultModel, _source.Token);
        setResuttResult.Should().Be(System.Net.HttpStatusCode.OK);

        await Task.Delay(1000);
        _latestPerformanceResults.Should().NotBeNull();
        _latestPerformanceResults.Content.Division.Should().Be("Senior Women");
        _latestPerformanceResults.Content.Competitors[0].Name.Should().Be("expected name");
        _latestPerformanceResults.Content.CurrentPlace.Should().Be(2);
    }
}
