using Api.Models;

namespace ConsoleClient.Util;

public static class Data
{
    public static Dictionary<int, PoleResultFileModel> CreateResults()
    {
        return new Dictionary<int, PoleResultFileModel>
        {
            {6, Create(66.266m, 72.833m, 17.000m, 0)}, //1
            {5, Create(59.966m, 70.500m, 16.900m, 0)}, //2
            {7, Create(60.833m, 69.166m, 16.000m, 0)}, //3
            {3, Create(62.000m, 68.333m, 15.000m, 0)}, //4
            {4, Create(62.066m, 70.333m, 11.600m, 0)}, //5
            {8, Create(60.466m, 69.666m, 13.300m, 0)}, //6
            {9, Create(61.933m, 71.500m, 7.700m, 0)}, //7
            {10, Create(57.166m, 70.000m, 13.900m, 0)}, //8
            {11, Create(59.766m, 66.833m, 12.800m, 0)}, //9
            {2, Create(56.666m, 69.500m, 10.900m, 2.00m)}, //10
        };
    }

    public static CompetitionFileModel CreateInitialData()
    {
        return new CompetitionFileModel
        {
            Name = "Testing competition",
            CurrentCompetitor = null,
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
                            Competitors = CreateSingleCompetitor("Competitor Who Has Forfeited", "Team 1"),
                            Forfeit = true
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 2,
                            Competitors = CreateSingleCompetitor("First in start order", null),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 3,
                            Competitors = CreateSingleCompetitor("Second Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 4,
                            Competitors = CreateSingleCompetitor("Third Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 5,
                            Competitors = CreateSingleCompetitor("Fourth Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 6,
                            Competitors = CreateSingleCompetitor("Fifth Person, I forfeit in middle", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 7,
                            Competitors = CreateSingleCompetitor("Sixth Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 8,
                            Competitors = CreateSingleCompetitor("Seventh Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 9,
                            Competitors = CreateSingleCompetitor("First in start order", null),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 10,
                            Competitors = CreateSingleCompetitor("Second Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 11,
                            Competitors = CreateSingleCompetitor("Third person", "my team"),
                        },
                    }
                },
                new DivisionFileModel
                {
                    Name = "Senior Men",
                    Items = new []
                    {
                        new CompetitorPositionFileModel
                        {
                            Id = 20,
                            Competitors = CreateSingleCompetitor("First in start order", null),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 21,
                            Competitors = CreateSingleCompetitor("Second Person", "my team"),
                        },
                        new CompetitorPositionFileModel
                        {
                            Id = 22,
                            Competitors = CreateSingleCompetitor("Third person", "my team"),
                        },
                    }
                }
            }
        };
    }

    private static PoleResultFileModel Create(decimal a, decimal e, decimal d, decimal hj) => new PoleResultFileModel
    {
        ArtisticScore = a,
        ExecutionScore = e,
        DifficultyScore = d,
        HeadJudgePenalty = hj
    };

    private static CompetitorFileModel[] CreateSingleCompetitor(string name, string? team)
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
