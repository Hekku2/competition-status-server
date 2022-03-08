using Org.OpenAPITools.Model;

namespace ConsoleClient.Util;

public static class Data
{
    public static Dictionary<int, PoleResultFileModel> CreateResults()
    {
        return new Dictionary<int, PoleResultFileModel>
        {
            {6, Create(66.266, 72.833, 17.000, 0)}, //1
            {5, Create(59.966, 70.500, 16.900, 0)}, //2
            {7, Create(60.833, 69.166, 16.000, 0)}, //3
            {3, Create(62.000, 68.333, 15.000, 0)}, //4
            {4, Create(62.066, 70.333, 11.600, 0)}, //5
            {8, Create(60.466, 69.666, 13.300, 0)}, //6
            {9, Create(61.933, 71.500, 7.700, 0)}, //7
            {10, Create(57.166, 70.000, 13.900, 0)}, //8
            {11, Create(59.766, 66.833, 12.800, 0)}, //9
            {2, Create(56.666, 69.500, 10.900, 2.00)}, //10

            {22, Create(63.966, 68.333, 15.600, 0)}, //1
            {21, Create(61.933, 65.333, 9.700, 0)}, //2
            {20, Create(62.666, 62.833, 10.300, 0)}, //3
        };
    }

    public static CompetitionFileModel CreateInitialData()
    {
        return new CompetitionFileModel(string.Empty, divisions: new List<DivisionFileModel>(), scoreboardSettings: new ScoreboardSettingsFileModel())
        {
            Name = "Testing competition",
            CurrentCompetitor = null,
            Divisions = new List<DivisionFileModel>
            {
                new DivisionFileModel(string.Empty, new List<CompetitorPositionFileModel>())
                {
                    Name = "Senior Women",
                    Items = new List<CompetitorPositionFileModel>
                    {
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 1,
                            Competitors = CreateSingleCompetitor("Competitor Who Has Forfeited", "Team 1"),
                            Forfeit = true
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 2,
                            Competitors = CreateSingleCompetitor("First in start order", null),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 3,
                            Competitors = CreateSingleCompetitor("Second Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 4,
                            Competitors = CreateSingleCompetitor("Third Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 5,
                            Competitors = CreateSingleCompetitor("Fourth Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 6,
                            Competitors = CreateSingleCompetitor("Fifth Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 7,
                            Competitors = CreateSingleCompetitor("Sixth Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 8,
                            Competitors = CreateSingleCompetitor("Seventh Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 9,
                            Competitors = CreateSingleCompetitor("First in start order", null),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 10,
                            Competitors = CreateSingleCompetitor("Second Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 11,
                            Competitors = CreateSingleCompetitor("Third person", "my team"),
                        },
                    }
                },
                new DivisionFileModel(string.Empty, new List<CompetitorPositionFileModel>())
                {
                    Name = "Senior Men",
                    Items = new List<CompetitorPositionFileModel>
                    {
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 20,
                            Competitors = CreateSingleCompetitor("First in start order", null),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 21,
                            Competitors = CreateSingleCompetitor("Second Person", "my team"),
                        },
                        new CompetitorPositionFileModel(competitors: new List<CompetitorFileModel>())
                        {
                            Id = 22,
                            Competitors = CreateSingleCompetitor("Third person", "my team"),
                        },
                    }
                }
            }
        };
    }

    private static PoleResultFileModel Create(double a, double e, double d, double hj) => new PoleResultFileModel
    {
        ArtisticScore = a,
        ExecutionScore = e,
        DifficultyScore = d,
        HeadJudgePenalty = hj
    };

    private static List<CompetitorFileModel> CreateSingleCompetitor(string name, string? team)
    {
        return new CompetitorFileModel[]
        {
            new CompetitorFileModel(string.Empty)
            {
                Name = name,
                Team = team
            }
        }.ToList();
    }
}
