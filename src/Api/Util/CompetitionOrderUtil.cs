using System;
using System.Linq;
using DataAccess.Entity;

namespace Api.Util;

public static class CompetitionOrderUtil
{

    public static int CalculatePlacement(CompetitionOrderEntity[] currentResults, int id)
    {
        // TODO Make this safer, test this, utilise this in competition controller mapping etc. This should also be refactored etc.
        var order = currentResults
            .Where(item => !item.Forfeit && item.Result is not null)
            .OrderByDescending(item => item.Result == null ? -9999 : item.Result.Total())
            .ToArray();

        for (var i = 0; i < order.Length; i++)
        {
            if (order[i].Id == id)
            {
                return i + 1;
            }
        }
        throw new ArgumentException($"No result found for ID #{id}");
    }

    private static decimal Total(this PoleDanceResultEntity entity)
    {
        return entity.ArtisticScore + entity.DifficultyScore + entity.ExecutionScore - entity.HeadJudgePenalty;
    }
}
