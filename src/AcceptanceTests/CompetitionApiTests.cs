using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AcceptanceTests.Util;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using NUnit.Framework;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Model;

namespace AcceptanceTests;

public class CompetitionApiTests : AcceptanceTestBase
{
    private CurrentCompetitorEnvelopeModel _latestMessage;

    protected override async Task OnSetup()
    {
        _latestMessage = null;

        var channel = await CompetitionHub.StreamAsChannelAsync<CurrentCompetitorEnvelopeModel>("StreamCompetitors");

        _ = channel.ReadUntilStopped((item) =>
        {
            _latestMessage = item;
        }, TokenSource.Token);
    }

    [Test]
    public async Task CreateCompetition_MinimalValues()
    {
        var model = new CompetitionFileModel("", new List<DivisionFileModel>(), scoreboardSettings: TestUtil.Minimal())
        {
            Name = "New competition",
            Divisions = new List<DivisionFileModel>()
        };
        await CompetitionApi.CompetitionUploadCompetitionAsync(model, TokenSource.Token);

        var response = await CompetitionApi.CompetitionGetCompetitionStatusAsync(TokenSource.Token);
        response.Type.Should().Be("competition-status");
        response._Version.Should().Be("1");
        response.Content.Should().NotBeNull();

        response.Content.EventName.Should().Be(model.Name);
        response.Content.CurrentCompetitor.Should().BeNull();
    }

    [Test]
    public async Task CreateCompetition_GeneralValues()
    {
        var model = new CompetitionFileModel("", new List<DivisionFileModel>(), scoreboardSettings: TestUtil.Minimal())
        {
            Name = "New competition",
            CurrentCompetitor = new CurrentCompetitorFileModel(division: "", competitors: new List<CompetitorFileModel>())
            {
                Id = 5,
                Competitors = TestUtil.CreateSingleCompetitor("Different 1", "my team"),
                Division = "Masters +40 Women"
            },
            Divisions = new List<DivisionFileModel>
            {
                new DivisionFileModel("", new List<CompetitorPositionFileModel>())
                {
                    Name = "Senior Women",
                    Items = new List<CompetitorPositionFileModel>
                    {
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 1,
                            Competitors = TestUtil.CreateSingleCompetitor("I should be last or second last", "my team"),
                            Results = null,
                            Forfeit = true
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 2,
                            Competitors = TestUtil.CreateSingleCompetitor("I should be second", "my team"),
                            Results = new PoleResultFileModel(0,0,0,0)
                            {
                                ArtisticScore = 100,
                                DifficultyScore = 10,
                                ExecutionScore = 60,
                                HeadJudgePenalty = 0
                            },
                            Forfeit = false
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 3,
                            Competitors = TestUtil.CreateSingleCompetitor("I should be first", "my team"),
                            Results = new PoleResultFileModel(0,0,0,0)
                            {
                                ArtisticScore = 100,
                                DifficultyScore = 10,
                                ExecutionScore = 90,
                                HeadJudgePenalty = 0
                            },
                            Forfeit = false
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 4,
                            Competitors = TestUtil.CreateSingleCompetitor("I also forfeited, my result should not be shown", "my team"),
                            Results = new PoleResultFileModel(0,0,0,0)
                            {
                                ArtisticScore = 900,
                                DifficultyScore = 10,
                                ExecutionScore = 990,
                                HeadJudgePenalty = 0
                            },
                            Forfeit = true
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 5,
                            Competitors = TestUtil.CreateSingleCompetitor("I'm upcoming 1", "my team"),
                            Results = null,
                            Forfeit = false
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 6,
                            Competitors = TestUtil.CreateSingleCompetitor("I'm upcoming 2", "my team"),
                            Results = null,
                            Forfeit = false
                        },
                    }
                }
            }
        };
        await CompetitionApi.CompetitionUploadCompetitionAsync(model, TokenSource.Token);

        var response = await CompetitionApi.CompetitionGetCompetitionStatusAsync(TokenSource.Token);

        response.Content.EventName.Should().Be(model.Name);
        response.Content.CurrentCompetitor.Should().NotBeNull("CurrentCompetitor should be defined");
        response.Content.CurrentCompetitor.Division.Should().Be("Masters +40 Women");
        response.Content.CurrentCompetitor.Competitors.Count.Should().Be(1);
        response.Content.CurrentCompetitor.Competitors.First().Name.Should().Be("Different 1");

        response.Content.Divisions.Count.Should().Be(1);
        var responseDivision = response.Content.Divisions.First();
        responseDivision.Name.Should().Be(model.Divisions.First().Name);

        responseDivision.Results.Count.Should().Be(2);
        responseDivision.Results.First().Competitors.Count.Should().Be(1);
        responseDivision.Results.First().Competitors.First().Name.Should().Be("I should be first");
        responseDivision.Results.First().Forfeit.Should().BeFalse();
        responseDivision.Results.Last().Competitors.Count.Should().Be(1);
        responseDivision.Results.Last().Competitors.First().Name.Should().Be("I should be second");
        responseDivision.Results.Last().Forfeit.Should().BeFalse();

        responseDivision.Forfeited.Count.Should().Be(2);
        responseDivision.Forfeited.First().Competitors.Count.Should().Be(1);
        responseDivision.Forfeited.First().Competitors.First().Name.Should().Be("I should be last or second last");
        responseDivision.Forfeited.First().Forfeit.Should().BeTrue();
        responseDivision.Forfeited.First().Result.Should().BeNull();
        responseDivision.Forfeited.Last().Competitors.Count.Should().Be(1);
        responseDivision.Forfeited.Last().Competitors.First().Name.Should().Be("I also forfeited, my result should not be shown");
        responseDivision.Forfeited.Last().Forfeit.Should().BeTrue();
        responseDivision.Forfeited.Last().Result.Should().BeNull();

        responseDivision.UpcomingCompetitorModels.Count.Should().Be(2);
        responseDivision.UpcomingCompetitorModels.First().Competitors.First().Name.Should().Be("I'm upcoming 1");
        responseDivision.UpcomingCompetitorModels.Last().Competitors.First().Name.Should().Be("I'm upcoming 2");
    }

    [Test]
    public async Task SetCurrentCompetitor_Normal()
    {
        var model = new CompetitionFileModel("", new List<DivisionFileModel>(), scoreboardSettings: TestUtil.Minimal())
        {
            Name = "New competition",
            Divisions = new List<DivisionFileModel>
            {
                    new DivisionFileModel("", new List<CompetitorPositionFileModel>())
                    {
                        Name = "Senior Women",
                        Items = new List<CompetitorPositionFileModel>
                        {
                            new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                            {
                                Id = 1,
                                Competitors = TestUtil.CreateSingleCompetitor("I should not be selected", "my team"),
                            },
                            new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                            {
                                Id = 2,
                                Competitors = TestUtil.CreateSingleCompetitor("I should be shown first", "my team"),
                            },
                            new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                            {
                                Id = 3,
                                Competitors = TestUtil.CreateSingleCompetitor("I should be shown second", "my team")
                            }
                        }
                    }
                }
        };
        await CompetitionApi.CompetitionUploadCompetitionAsync(model, TokenSource.Token);

        var shoudlBeNull = await CompetitionApi.CompetitionGetCurrentCompetitorAsync(TokenSource.Token);
        shoudlBeNull.Content.Should().BeNull();

        _latestMessage.Content.Should().BeNull();

        await CompetitionApi.CompetitionSetCurrentCompetitorAsync(new CurrentCompetitorSetModel { Id = 2 }, TokenSource.Token);

        Thread.Sleep(1000);
        _latestMessage.Content.Should().NotBeNull();

        var first = await CompetitionApi.CompetitionGetCurrentCompetitorAsync(TokenSource.Token);
        first.Content.Should().NotBeNull();
        first.Content.Division.Should().Be("Senior Women");
        first.Content.Competitors.First().Name.Should().Be("I should be shown first");

        await CompetitionApi.CompetitionSetCurrentCompetitorAsync(new CurrentCompetitorSetModel { Id = 3 }, TokenSource.Token);

        var second = await CompetitionApi.CompetitionGetCurrentCompetitorAsync(TokenSource.Token);
        second.Content.Should().NotBeNull();
        second.Content.Division.Should().Be("Senior Women");
        second.Content.Competitors.First().Name.Should().Be("I should be shown second");
    }
}
