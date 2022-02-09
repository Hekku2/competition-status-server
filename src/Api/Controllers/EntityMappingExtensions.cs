using System;
using System.Linq;
using Api.Models;
using DataAccess.Entity;

namespace Api.Controllers
{
    public static class EntityMappingExtensions
    {
        public static CompetitionStatusContentModel ToCompetitionStatusContentModel(this CompetitionEntity entity) => new()
        {
            EventName = entity.Name,
            CreatedAt = DateTime.UtcNow.ToString(),
            Divisions = entity.Divisions.Select(ToDivisionStatusModel).ToArray(),
            CurrentCompetitor = entity.CurrentCompetitor != null ? ToCurrentCompetitorContentModel(entity.CurrentCompetitor) : null
        };

        public static CurrentCompetitorContentModel ToCurrentCompetitorContentModel(this CurrentCompetitorsEntity entity) => new()
        {
            Division = entity.Division,
            Competitors = entity.Competitors.Select(ToCompetitorModel).ToArray()
        };

        public static DivisionStatusModel ToDivisionStatusModel(this DivisionEntity entity) => new()
        {
            Name = entity.Name,
            Results = entity
                .CompetitionOrder
                .Where(item => item.Forfeit || item.Result is not null)
                .Select(ToResultRowModel)
                .OrderByDescending(item => item.Forfeit ? -9999 : item.Result?.Total)
                .ToArray(),
            UpcomingCompetitorModels = entity
                .CompetitionOrder
                .Where(item => !item.Forfeit && item.Result is null)
                .Select(ToUpcomingCompetitorModel)
                .ToArray()
        };

        public static UpcomingCompetitorModel ToUpcomingCompetitorModel(this CompetitionOrderEntity entity) => new()
        {
            Id = entity.Id,
            Competitors = entity.Competitors.Select(ToCompetitorModel).ToArray()
        };

        public static CompetitorModel ToCompetitorModel(this CompetitorEntity entity) => new()
        {
            Name = entity.Name,
            Team = entity.Team
        };

        public static PoleSportResultModel ToPoleSportResultModel(this PoleDanceResultEntity model) => new()
        {
            ArtisticScore = model.ArtisticScore,
            DifficultyScore = model.DifficultyScore,
            ExecutionScore = model.ExecutionScore,
            HeadJudgePenalty = model.HeadJudgePenalty,
            Total = model.ArtisticScore + model.DifficultyScore + model.ExecutionScore - model.HeadJudgePenalty
        };

        public static ResultRowModel ToResultRowModel(this CompetitionOrderEntity entity) => new()
        {
            Id = entity.Id,
            Competitors = entity.Competitors.Select(ToCompetitorModel).ToArray(),
            Result = entity.Forfeit ? null : entity.Result?.ToPoleSportResultModel(),
            Forfeit = entity.Forfeit
        };
    }
}
