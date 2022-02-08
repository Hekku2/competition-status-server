using System;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Api.Models;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using NUnit.Framework;
using RestSharp;

namespace AcceptanceTests
{
    public class CompetitionApiTests
    {
        private RestClient _client;
        private ChannelReader<CurrentCompetitorEnvelopeModel> _channel;

        private CurrentCompetitorEnvelopeModel _latestMessage;
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
            _latestMessage = null;

            var connection = new HubConnectionBuilder()
                .WithUrl($"{uri}competition-hub")
                .Build();

            await connection.StartAsync();
            _channel = await connection.StreamAsChannelAsync<CurrentCompetitorEnvelopeModel>("StreamCompetitors");

            _source = new CancellationTokenSource();
            var x = ReadEventsToQueue(_source.Token);

            _client = new RestClient($"{uri}{ApiName}");
        }

        [TearDown]
        public void TearDown()
        {
            _source.Cancel();
        }


        [Test]
        public async Task CreateCompetition_MinimalValues()
        {
            var model = new CompetitionFileModel
            {
                Name = "New competition",
                Divisions = Array.Empty<DivisionFileModel>()
            };
            await _client.PostJsonAsync("upload-competition", model, CancellationToken.None);

            var response = await _client.GetJsonAsync<CompetitionStatusEnvelopeModel>("competition-status", CancellationToken.None);
            response.Type.Should().Be("competition-status");
            response.Version.Should().Be("1");
            response.Content.Should().NotBeNull();

            response.Content.EventName.Should().Be(model.Name);
        }

        [Test]
        public async Task CreateCompetition_GeneralValues()
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
                                        Competitors = CreateSingleCompetitor("I should not be on list", "my team"),
                                        Results = null,
                                        Forfeit = true
                                    },
                                    new CompetitorPositionFileModel
                                    {
                                        Competitors = CreateSingleCompetitor("I should be second", "my team"),
                                        Results = new PoleResultFileModel
                                        {
                                            ArtisticScore = 100,
                                            DifficultyScore = 10,
                                            ExecutionScore = 60,
                                            HeadJudgePenalty = 0
                                        },
                                        Forfeit = false
                                    },
                                    new CompetitorPositionFileModel
                                    {
                                        Competitors = CreateSingleCompetitor("I should be first", "my team"),
                                        Results = new PoleResultFileModel
                                        {
                                            ArtisticScore = 100,
                                            DifficultyScore = 10,
                                            ExecutionScore = 90,
                                            HeadJudgePenalty = 0
                                        },
                                        Forfeit = false
                                    },
                                    new CompetitorPositionFileModel
                                    {
                                        Competitors = CreateSingleCompetitor("I also forfeited, my result should not be shown", "my team"),
                                        Results = new PoleResultFileModel
                                        {
                                            ArtisticScore = 900,
                                            DifficultyScore = 10,
                                            ExecutionScore = 990,
                                            HeadJudgePenalty = 0
                                        },
                                        Forfeit = true
                                    },
                                    new CompetitorPositionFileModel
                                    {
                                        Competitors = CreateSingleCompetitor("I'm upcoming 1", "my team"),
                                        Results = null,
                                        Forfeit = false
                                    },
                                    new CompetitorPositionFileModel
                                    {
                                        Competitors = CreateSingleCompetitor("I'm upcoming 2", "my team"),
                                        Results = null,
                                        Forfeit = false
                                    },
                                }
                            }
                        }
            };
            var result = await _client.PostJsonAsync("upload-competition", model, CancellationToken.None);
            result.Should().Be(System.Net.HttpStatusCode.OK);

            var response = await _client.GetJsonAsync<CompetitionStatusEnvelopeModel>("competition-status", CancellationToken.None);

            response.Content.EventName.Should().Be(model.Name);
            response.Content.Divisions.Length.Should().Be(1);
            var responseDivision = response.Content.Divisions.First();
            responseDivision.Name.Should().Be(model.Divisions[0].Name);

            responseDivision.Results.Length.Should().Be(2);
            responseDivision.Results[0].Competitors.Length.Should().Be(1);
            responseDivision.Results[0].Competitors[0].Name.Should().Be("I should be first");
            responseDivision.Results[1].Competitors.Length.Should().Be(1);
            responseDivision.Results[1].Competitors[0].Name.Should().Be("I should be second");

            responseDivision.UpcomingCompetitorModels.Length.Should().Be(2);
            responseDivision.UpcomingCompetitorModels[0].Competitors[0].Name.Should().Be("I'm upcoming 1");
            responseDivision.UpcomingCompetitorModels[1].Competitors[0].Name.Should().Be("I'm upcoming 2");
        }

        [Test]
        public async Task SetCurrentCompetitor_Normal()
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
                                Competitors = CreateSingleCompetitor("I should not be selected", "my team"),
                            },
                            new CompetitorPositionFileModel
                            {
                                Id = 2,
                                Competitors = CreateSingleCompetitor("I should be shown first", "my team"),
                            },
                            new CompetitorPositionFileModel
                            {
                                Id = 3,
                                Competitors = CreateSingleCompetitor("I should be shown second", "my team")
                            }
                        }
                    }
                }
            };
            await _client.PostJsonAsync("upload-competition", model, CancellationToken.None);

            var shoudlBeNull = await _client.GetJsonAsync<CurrentCompetitorEnvelopeModel>("current-competitor", CancellationToken.None);
            shoudlBeNull.Content.Should().BeNull();

            _latestMessage.Content.Should().BeNull();

            await _client.PostJsonAsync("set-current-competitor", new CurrentCompetitorSetModel { Id = 2 }, CancellationToken.None);

            Thread.Sleep(1000);
            _latestMessage.Content.Should().NotBeNull();

            var first = await _client.GetJsonAsync<CurrentCompetitorEnvelopeModel>("current-competitor", CancellationToken.None);
            first.Content.Should().NotBeNull();
            first.Content.Division.Should().Be("Senior Women");
            first.Content.Competitors[0].Name.Should().Be("I should be shown first");

            await _client.PostJsonAsync("set-current-competitor", new CurrentCompetitorSetModel { Id = 3 }, CancellationToken.None);

            var second = await _client.GetJsonAsync<CurrentCompetitorEnvelopeModel>("current-competitor", CancellationToken.None);
            second.Content.Should().NotBeNull();
            second.Content.Division.Should().Be("Senior Women");
            second.Content.Competitors[0].Name.Should().Be("I should be shown second");
        }

        private static CompetitorFileModel[] CreateSingleCompetitor(string name, string team)
        {
            return new CompetitorFileModel[]
            {
                new CompetitorFileModel
                {
                    Name = name,
                    Team = team
                }
            };
        }

        private async Task ReadEventsToQueue(CancellationToken token)
        {
            while (await _channel.WaitToReadAsync(token))
            {
                while (_channel.TryRead(out var receivedEvent))
                {
                    _latestMessage = receivedEvent;
                }
            }
        }
    }
}
