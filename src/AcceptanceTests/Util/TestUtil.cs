using Api.Models;

namespace AcceptanceTests.Util;

public static class TestUtil
{
    public static CompetitorFileModel[] CreateSingleCompetitor(string name, string team)
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
