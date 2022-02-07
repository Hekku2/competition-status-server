using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace AcceptanceTests
{
    public class CompetitionApiTests
    {
        private RestClient _client;
        private const string ApiName = "Competition";

        [SetUp]
        public void Setup()
        {
            var uri = Environment.GetEnvironmentVariable("APP_URI");
            //var uri = "http://localhost:5000/";
            _client = new RestClient($"{uri}{ApiName}");
        }

        [Test]
        public async Task CurrentCompetitor_IsNullAtStart()
        {
            var response = await _client.GetJsonAsync<CurrentCompetitorEnvelopeModel>("current-competitor", CancellationToken.None);
            response.Type.Should().Be("current-competitor");
            response.Version.Should().Be("1");
            response.Content.Should().BeNull();
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
                                Competitors = CreateSingleCompetitor("I also forfeited, my resuylt should not be shown", "my team"),
                                Results = new PoleResultFileModel
                                {
                                    ArtisticScore = 900,
                                    DifficultyScore = 10,
                                    ExecutionScore = 990,
                                    HeadJudgePenalty = 0
                                },
                                Forfeit = true
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
    }
}
