using Api.Controllers;
using Api.Models;
using Api.Services.Interfaces;
using DataAccess.Entity;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Api.Tests;

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
