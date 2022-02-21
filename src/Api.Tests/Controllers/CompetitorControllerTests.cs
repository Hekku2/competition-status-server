using System;
using System.Linq;
using Api.Services.Interfaces;
using DataAccess.Entity;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Api.Controllers.Tests;

public class CompetitorControllerTests
{
    private ICompetitionService _mockCompetitionService;

    private CompetitorController _controller;

    [SetUp]
    public void Setup()
    {
        var logger = Substitute.For<ILogger<CompetitorController>>();
        _mockCompetitionService = Substitute.For<ICompetitionService>();
        _controller = new CompetitorController(logger, _mockCompetitionService);
    }

    [Test]
    public void GetAll_WhenCompetitionNotActive_ReturnsEmpty()
    {
        var result = _controller.GetAll();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public void GetAll_WhenCompetitionActiveWithNoDivisions_ReturnsEmpty()
    {
        var competition = new CompetitionEntity
        {
            Divisions = Array.Empty<DivisionEntity>()
        };
        _mockCompetitionService.GetCurrentState().Returns(competition);

        var result = _controller.GetAll();

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Test]
    public void GetAll_SingleCompetition_MapsValuesCorrectly()
    {
        var competition = new CompetitionEntity
        {
            Divisions = new[]
            {
                new DivisionEntity
                {
                    Name = "test division",
                    CompetitionOrder = new []
                    {
                        new CompetitionOrderEntity
                        {
                            Id = 1,
                            Competitors = new []
                            {
                                new CompetitorEntity
                                {
                                    Name = "Competitor 1",
                                    Team = "Team 1"
                                },
                                new CompetitorEntity
                                {
                                    Name = "Competitor 2",
                                    Team = "Team 2"
                                }
                            }
                        },
                        new CompetitionOrderEntity
                        {
                            Id = 2,
                            Competitors = new []
                            {
                                new CompetitorEntity
                                {
                                    Name = "Competitor 3",
                                    Team = "Team 3"
                                }
                            },
                            Forfeit = true
                        }
                    }
                },
                new DivisionEntity
                {
                    Name = "test division 2",
                    CompetitionOrder = new []
                    {
                        new CompetitionOrderEntity
                        {
                            Id = 3,
                            Competitors = new []
                            {
                                new CompetitorEntity
                                {
                                    Name = "Competitor 4",
                                    Team = "Team 4"
                                }
                            },
                            Forfeit = false
                        }
                    }
                }
            }
        };
        _mockCompetitionService.GetCurrentState().Returns(competition);

        var result = _controller.GetAll();

        result.Length.Should().Be(3);
        var first = result.First(item => item.Id == 1);
        first.Division.Should().Be("test division");
        first.Forfeit.Should().Be(false);
        first.Competitors.Length.Should().Be(2);
        first.Competitors[0].Name.Should().Be("Competitor 1");
        first.Competitors[0].Team.Should().Be("Team 1");
        first.Competitors[1].Name.Should().Be("Competitor 2");
        first.Competitors[1].Team.Should().Be("Team 2");

        var second = result.First(item => item.Id == 2);
        second.Forfeit.Should().Be(true);
        second.Division.Should().Be("test division");
        second.Competitors.Length.Should().Be(1);
        second.Competitors[0].Name.Should().Be("Competitor 3");
        second.Competitors[0].Team.Should().Be("Team 3");


        var third = result.First(item => item.Id == 3);
        third.Forfeit.Should().Be(false);
        third.Division.Should().Be("test division 2");
        third.Competitors.Length.Should().Be(1);
        third.Competitors[0].Name.Should().Be("Competitor 4");
        third.Competitors[0].Team.Should().Be("Team 4");
    }
}
