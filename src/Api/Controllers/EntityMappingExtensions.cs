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

        public static CompetitionFileModel ToCompetitionFileModel(this CompetitionEntity entity) => new()
        {
            Name = entity.Name,
            Divisions = entity.Divisions.Select(ToDivisionFileModel).ToArray(),
            CurrentCompetitor = entity.CurrentCompetitor != null ? ToCurrentCompetitorFileModel(entity.CurrentCompetitor) : null
        };

        public static CurrentCompetitorContentModel ToCurrentCompetitorContentModel(this CurrentCompetitorsEntity entity) => new()
        {
            Division = entity.Division,
            Competitors = entity.Competitors.Select(ToCompetitorModel).ToArray()
        };

        public static CurrentCompetitorFileModel ToCurrentCompetitorFileModel(this CurrentCompetitorsEntity entity) => new()
        {
            Division = entity.Division,
            Competitors = entity.Competitors.Select(ToCompetitorFileModel).ToArray()
        };

        public static DivisionFileModel ToDivisionFileModel(this DivisionEntity entity) => new()
        {
            Name = entity.Name,
            Items = entity.CompetitionOrder.Select(ToCompetitorPositionFileModel).ToArray()
        };

        public static CompetitorPositionFileModel ToCompetitorPositionFileModel(this CompetitionOrderEntity entity) => new()
        {
            Id = entity.Id,
            Competitors = entity.Competitors.Select(ToCompetitorFileModel).ToArray(),
            Forfeit = entity.Forfeit,
            Results = entity.Result?.ToPoleResultFileModel()
        };

        public static CompetitorFileModel ToCompetitorFileModel(this CompetitorEntity entity) => new()
        {
            Name = entity.Name,
            Team = entity.Team
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

        public static PoleSportResultModel ToPoleSportResultModel(this PoleDanceResultEntity entity) => new()
        {
            ArtisticScore = entity.ArtisticScore,
            DifficultyScore = entity.DifficultyScore,
            ExecutionScore = entity.ExecutionScore,
            HeadJudgePenalty = entity.HeadJudgePenalty,
            Total = entity.ArtisticScore + entity.DifficultyScore + entity.ExecutionScore - entity.HeadJudgePenalty
        };

        public static PoleResultFileModel ToPoleResultFileModel(this PoleDanceResultEntity entity) => new()
        {
            ArtisticScore = entity.ArtisticScore,
            DifficultyScore = entity.DifficultyScore,
            ExecutionScore = entity.ExecutionScore,
            HeadJudgePenalty = entity.HeadJudgePenalty,
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
