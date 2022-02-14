using System.Linq;
using DataAccess.Entity;

namespace Api.Util;

public static class CompetitionOrderUtil
{
    public static int? CalculatePlacement(CompetitionOrderEntity[] currentResults, int id)
    {
        var order = CalculateOrder(currentResults);
        for (var i = 0; i < order.Length; i++)
        {
            if (order[i].Id == id)
            {
                return i + 1;
            }
        }
        return null;
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
