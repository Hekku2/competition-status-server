using Api.Models;

namespace ConsoleClient.Util;

public static class Data
{
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
                    }
                },
                new DivisionFileModel
                {
                    Name = "Senior Men",
                    Items = new []
                    {
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
                }
            }
        };
    }

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
