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
}
