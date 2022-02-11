using System;
using System.Linq;
using DataAccess.Entity;

namespace Api.Util;

public static class CompetitionOrderUtil
{

    public static int CalculatePlacement(CompetitionOrderEntity[] currentResults, int id)
    {
        var order = CalculateOrder(currentResults);
        // TODO Make this safer, test this, utilise this in competition controller mapping etc. This should also be refactored etc.

        for (var i = 0; i < order.Length; i++)
        {
            if (order[i].Id == id)
            {
                return i + 1;
            }
        }
        throw new ArgumentException($"No result found for ID #{id}");
    }

    public static CompetitionOrderEntity[] CalculateOrder(CompetitionOrderEntity[] currentResults)
    {
        return currentResults
            .Where(item => !item.Forfeit && item.Result is not null)
            .OrderByDescending(item => item.Result?.Total())
            .ToArray();
    }

    public static decimal Total(this PoleDanceResultEntity entity)
    {
        return entity.ArtisticScore + entity.DifficultyScore + entity.ExecutionScore - entity.HeadJudgePenalty;
    }
}
