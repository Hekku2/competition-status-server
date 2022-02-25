using System.Collections.Generic;
using System.Linq;
using Org.OpenAPITools.Model;

namespace AcceptanceTests.Util;

public static class TestUtil
{
    public static List<CompetitorFileModel> CreateSingleCompetitor(string name, string team)
    {
        return new CompetitorFileModel[]
        {
            new CompetitorFileModel(name, team)
        }.ToList();
    }

    public static CompetitionFileModel CreateDataModel()
    {
        return new CompetitionFileModel("", new List<DivisionFileModel>())
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
    }
}
