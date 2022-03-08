using Api.Models;
using Api.Services.Interfaces;
using DataAccess.Entity;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Api.Controllers.Tests;

public class ScoreboardControllerTests
{
    private IScoreboardService _mockScoreboardService;

    private ScoreboardController _controller;

    [SetUp]
    public void Setup()
    {
        _mockScoreboardService = Substitute.For<IScoreboardService>();

        _controller = new ScoreboardController(_mockScoreboardService);
    }

    [Test]
    public void GetStatus_ReturnsStatus()
    {
        var expected = ScoreboardMode.DivisionStatus;
        _mockScoreboardService.GetScoreboardMode().Returns(expected);

        var result = _controller.GetStatus();

        result.Should().NotBeNull();
        result.ScoreboardMode.Should().Be(ScoreboardModeModel.DivisionStatus);
    }

    [Test]
    public void SetScoreboardMode_CallsService()
    {
        _controller.SetScoreboardMode(ScoreboardModeModel.DivisionStatus);

        _mockScoreboardService.Received().SetScoreboardMode(ScoreboardMode.DivisionStatus);
    }
}
