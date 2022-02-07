using Api.Models;
using Api.Services.Interfaces;
using DataAccess.Entity;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Api.Controllers.Tests;

public class CompetitionControllerTests
{
    private ICompetitionStatusService _mockCompetitionStatusService;
    private ICompetitionService _mockCompetitionService;

    private CompetitionController _controller;

    [SetUp]
    public void Setup()
    {
        var logger = Substitute.For<ILogger<CompetitionController>>();
        _mockCompetitionStatusService = Substitute.For<ICompetitionStatusService>();
        _mockCompetitionService = Substitute.For<ICompetitionService>();
        _controller = new CompetitionController(logger, _mockCompetitionStatusService, _mockCompetitionService);
    }

    [Test]
    public void GetCompetitionStatus_SetsResultsCorrectly()
    {
        var expectedCompetition = new CompetitionEntity
        {
            Name = "Name of competition",
            Divisions = new[]
            {
                new DivisionEntity
                {
                    Name = "Name of division",
                    CompetitionOrder = new []
                    {
                        new CompetitionOrderEntity
                        {
                            Competitors = CompetitorEntity("second", "team 1"),
                            Result = new PoleDanceResultEntity
                            {
                                ArtisticScore = 1,
                                DifficultyScore = 10,
                                ExecutionScore = 100,
                                HeadJudgePenalty = 5
                            }
                        },
                        new CompetitionOrderEntity
                        {
                            Competitors = CompetitorEntity("first", "team 1"),
                            Result = new PoleDanceResultEntity
                            {
                                ArtisticScore = 100,
                                DifficultyScore = 10,
                                ExecutionScore = 100,
                                HeadJudgePenalty = 5
                            }
                        }
                    }
                }
            }
        };
        _mockCompetitionService.GetCurrentState().Returns(expectedCompetition);

        var actualCompetitionEnvelope = _controller.GetCompetitionStatus();
        actualCompetitionEnvelope.Content.Divisions[0].Results[0].Competitors[0].Name.Should().Be("first");
        actualCompetitionEnvelope.Content.Divisions[0].Results[0].Result.Total.Should().Be(205);
        actualCompetitionEnvelope.Content.Divisions[0].Results[1].Competitors[0].Name.Should().Be("second");
        actualCompetitionEnvelope.Content.Divisions[0].Results[1].Result.Total.Should().Be(106);
    }

    [Test]
    public void GetCompetitionStatus_SetsUpcomingCompetitorModelsCorrectly()
    {
        var expectedCompetition = new CompetitionEntity
        {
            Name = "Name of competition",
            Divisions = new[]
            {
                new DivisionEntity
                {
                    Name = "Name of division",
                    CompetitionOrder = new []
                    {
                        new CompetitionOrderEntity
                        {
                            Competitors = CompetitorEntity("wrong", "team 1"),
                            Result = new PoleDanceResultEntity
                            {   // I'm not shown because I have already competed
                                ArtisticScore = 1,
                                DifficultyScore = 10,
                                ExecutionScore = 100,
                                HeadJudgePenalty = 5
                            }
                        },
                        new CompetitionOrderEntity
                        {   // I'm not shown because I have forfeited
                            Competitors = CompetitorEntity("wrong", "team 1"),
                            Forfeit = true
                        },
                        new CompetitionOrderEntity
                        {
                            Competitors = CompetitorEntity("first", "team 1"),
                        },
                        new CompetitionOrderEntity
                        {
                            Competitors = CompetitorEntity("second", "team 1"),
                        }
                    }
                }
            }
        };
        _mockCompetitionService.GetCurrentState().Returns(expectedCompetition);

        var actualCompetitionEnvelope = _controller.GetCompetitionStatus();
        actualCompetitionEnvelope.Content.Divisions[0].UpcomingCompetitorModels.Length.Should().Be(2);
        actualCompetitionEnvelope.Content.Divisions[0].UpcomingCompetitorModels[0].Competitors[0].Name.Should().Be("first");
        actualCompetitionEnvelope.Content.Divisions[0].UpcomingCompetitorModels[1].Competitors[0].Name.Should().Be("second");
    }

    [Test]
    public void GetCompetitionStatus_MapsEntityCorrectly()
    {
        var expectedCompetition = new CompetitionEntity
        {
            Name = "Name of competition",
            Divisions = new[]
            {
                new DivisionEntity
                {
                    Name = "Name of division",
                    CompetitionOrder = new []
                    {
                        new CompetitionOrderEntity
                        {   // I don't have results
                            Competitors = CompetitorEntity("wrong", "team wrong"),
                        },
                        new CompetitionOrderEntity
                        {   // I have forfeited
                            Competitors = CompetitorEntity("wrong", "team wrong"),
                            Forfeit = true,
                            Result = new PoleDanceResultEntity
                            {
                                ArtisticScore = 123,
                                DifficultyScore = 554,
                                ExecutionScore = 333,
                                HeadJudgePenalty = 34
                            }
                        },
                        new CompetitionOrderEntity
                        {
                            Competitors = CompetitorEntity("name 1", "team 1"),
                            Result = new PoleDanceResultEntity
                            {
                                ArtisticScore = 123,
                                DifficultyScore = 554,
                                ExecutionScore = 333,
                                HeadJudgePenalty = 34
                            }
                        }
                    }
                }
            }
        };
        _mockCompetitionService.GetCurrentState().Returns(expectedCompetition);
    }

    private static CompetitorEntity[] CompetitorEntity(string name, string team)
    {
        return new[]
        {
            new CompetitorEntity
            {
                Name = name,
                Team = team
            }
        };
    }

    [Test]
    public void UploadCompetition_MapsModelCorrectly()
    {
        var expectedDivision = new DivisionFileModel
        {
            Name = "Senior Women",
            Items = new[]
            {
                new CompetitorPositionFileModel
                {
                    Competitors = new CompetitorFileModel[]
                    {
                        new CompetitorFileModel
                        {
                            Name = "single competitor",
                            Team = "team",
                        }
                    },
                    Results = null,
                    Forfeit = true
                },
                new CompetitorPositionFileModel
                {
                    Competitors = new CompetitorFileModel[]
                    {
                        new CompetitorFileModel
                        {
                            Name = "double competitor 1",
                            Team = "team 1",
                        },
                        new CompetitorFileModel
                        {
                            Name = "double competitor 2",
                            Team = "team 2",
                        }
                    },
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
                    Competitors = new CompetitorFileModel[]
                    {
                        new CompetitorFileModel
                        {
                            Name = "single competitor",
                            Team = "team",
                        }
                    },
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
        };

        var model = new CompetitionFileModel
        {
            Name = "New competition",
            Divisions = new[]
            {
                expectedDivision
            }
        };

        CompetitionEntity saved = null;

        _mockCompetitionService.When(x => x.UploadCompetition(Arg.Any<CompetitionEntity>())).Do(x => saved = x.Arg<CompetitionEntity>());

        _controller.UploadCompetition(model);

        saved.Should().NotBeNull();
        saved.Name.Should().Be(model.Name);
        saved.Divisions.Length.Should().Be(1);

        var actualDivision = saved.Divisions[0];
        actualDivision.Name.Should().Be(expectedDivision.Name);
        actualDivision.CompetitionOrder.Length.Should().Be(expectedDivision.Items.Length);

        var expectedFirst = expectedDivision.Items[0];
        var actualFirst = actualDivision.CompetitionOrder[0];
        actualFirst.Forfeit.Should().BeTrue();
        actualFirst.Result.Should().BeNull();
        actualFirst.Competitors.Length.Should().Be(expectedFirst.Competitors.Length);
        actualFirst.Competitors[0].Name.Should().Be(expectedFirst.Competitors[0].Name);
        actualFirst.Competitors[0].Team.Should().Be(expectedFirst.Competitors[0].Team);

        var expectedSecond = expectedDivision.Items[1];
        var actualSecond = actualDivision.CompetitionOrder[1];

        actualSecond.Forfeit.Should().BeFalse();
        actualSecond.Result.Should().NotBeNull();
        actualSecond.Result.ArtisticScore.Should().Be(expectedSecond.Results.ArtisticScore);
        actualSecond.Result.DifficultyScore.Should().Be(expectedSecond.Results.DifficultyScore);
        actualSecond.Result.ExecutionScore.Should().Be(expectedSecond.Results.ExecutionScore);
        actualSecond.Result.HeadJudgePenalty.Should().Be(expectedSecond.Results.HeadJudgePenalty);
    }
}
