using System;
using DataAccess.Entity;
using FluentAssertions;
using NUnit.Framework;

namespace Api.Util.Tests;

public class CompetitionOrderUtilTests
{
    [Test]
    public void CalculateOrder_WhenEmpty_ReturnsEmpty()
    {
        var actual = CompetitionOrderUtil.CalculateOrder(Array.Empty<CompetitionOrderEntity>());

        actual.Should().BeEmpty();
    }

    [Test]
    public void CalculateOrder_WhenNoResults_ReturnEmpty()
    {
        var items = new[]
        {
            new CompetitionOrderEntity()
        };

        var actual = CompetitionOrderUtil.CalculateOrder(items);

        actual.Should().BeEmpty();
    }

    [Test]
    public void CalculateOrder_WhenSingleResult_IsFirst()
    {
        var items = new[]
        {
            new CompetitionOrderEntity
            {
                Result = new PoleDanceResultEntity()
            }
        };

        var actual = CompetitionOrderUtil.CalculateOrder(items);

        actual.Length.Should().Be(1);
        actual[0].Should().Be(items[0]);
    }

    [Test]
    public void CalculateOrder_WhenForfeit_ReturnsEmpty()
    {
        var items = new[]
        {
            new CompetitionOrderEntity
            {
                Forfeit = true
            },
            new CompetitionOrderEntity
            {
                Result = new PoleDanceResultEntity(),
                Forfeit = true
            }
        };

        var actual = CompetitionOrderUtil.CalculateOrder(items);
        actual.Should().BeEmpty();
    }

    [Test]
    public void CalculateOrder_WhenMultiple_SortsCorrectly()
    {
        var items = new[]
        {
            new CompetitionOrderEntity
            {
                Result = CreateResult(10, 20, 30, 0) //60
            },
            new CompetitionOrderEntity
            {
                Result = CreateResult(30, 20, 30, 0) //80
            },
            new CompetitionOrderEntity
            {
                Result = CreateResult(30, 20, 31, 2) //79
            },
            new CompetitionOrderEntity
            {
                Result = CreateResult(1, 1, 1, 2) //1
            }
        };

        var actual = CompetitionOrderUtil.CalculateOrder(items);
        actual[0].Should().Be(items[1]);
        actual[1].Should().Be(items[2]);
        actual[2].Should().Be(items[0]);
        actual[3].Should().Be(items[3]);
    }

    private static PoleDanceResultEntity CreateResult(decimal a, decimal e, decimal d, decimal hj) => new PoleDanceResultEntity
    {
        ArtisticScore = a,
        ExecutionScore = e,
        DifficultyScore = d,
        HeadJudgePenalty = hj
    };
}

public class EntityMappingExtensionsTests
{
    [Test]
    public void ToDivisionStatusModel_WhenForfeit_NotInResult()
    {
        var expected = new DivisionEntity
        {
            CompetitionOrder = new[]
            {
                new CompetitionOrderEntity
                {
                    Competitors = Array.Empty<CompetitorEntity>(),
                    Forfeit = true
                },
                new CompetitionOrderEntity
                {
                    Competitors = Array.Empty<CompetitorEntity>(),
                    Result = new PoleDanceResultEntity(),
                    Forfeit = true
                }
            }
        };

        var actual = expected.ToDivisionStatusModel();

        actual.Results.Should().BeEmpty();
        actual.Forfeited.Length.Should().Be(2);
    }
}
