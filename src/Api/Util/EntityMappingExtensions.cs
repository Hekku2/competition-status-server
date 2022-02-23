using System;
using System.Linq;
using Api.Models;
using DataAccess.Entity;

namespace Api.Util;

public static class EntityMappingExtensions
{
    public static CompetitionStatusContentModel ToCompetitionStatusContentModel(this CompetitionEntity entity) => new()
    {
        EventName = entity.Name,
        CreatedAt = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"),
        Divisions = entity.Divisions.Select(ToDivisionStatusModel).ToArray(),
        CurrentCompetitor = entity.CurrentCompetitor?.ToCurrentCompetitorContentModel()
    };

    public static CompetitionFileModel ToCompetitionFileModel(this CompetitionEntity entity) => new()
    {
        Name = entity.Name,
        Divisions = entity.Divisions.Select(ToDivisionFileModel).ToArray(),
        CurrentCompetitor = entity.CurrentCompetitor?.ToCurrentCompetitorFileModel()
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
        Results = CompetitionOrderUtil.CalculateOrder(entity
            .CompetitionOrder)
            .Select(ToResultRowModel)
            .ToArray(),
        UpcomingCompetitorModels = entity.CompetitionOrder.ToUpcomingCompetitorModelArray(),
        Forfeited = entity
            .CompetitionOrder
            .Where(item => item.Forfeit)
            .Select(ToResultRowModel)
            .ToArray()
    };

    public static UpcomingCompetitorModel[] ToUpcomingCompetitorModelArray(this CompetitionOrderEntity[] entities)
    {
        return entities
            .Where(item => !item.Forfeit && item.Result is null)
            .Select(ToUpcomingCompetitorModel)
            .ToArray();
    }

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
        Total = entity.Total()
    };

    public static PoleResultFileModel ToPoleResultFileModel(this PoleDanceResultEntity entity) => new()
    {
        ArtisticScore = entity.ArtisticScore,
        DifficultyScore = entity.DifficultyScore,
        ExecutionScore = entity.ExecutionScore,
        HeadJudgePenalty = entity.HeadJudgePenalty,
    };

    public static ParticipationRowModel ToResultRowModel(this CompetitionOrderEntity entity) => new()
    {
        Id = entity.Id,
        Competitors = entity.Competitors.Select(ToCompetitorModel).ToArray(),
        Result = entity.Forfeit ? null : entity.Result?.ToPoleSportResultModel(),
        Forfeit = entity.Forfeit
    };

    public static PerformanceResultsContentModel ToPerformanceResultsContentModel(this PerformanceResultsEntity entity) => new()
    {
        Division = entity.Division,
        CurrentPlace = entity.CurrentPlace,
        Competitors = entity.Competitors.Select(ToCompetitorModel).ToArray(),
        Result = entity.Result.ToPoleSportResultModel()
    };

    public static ScoreboardModeModel ToScoreboardModeModel(this ScoreboardMode entity) => entity switch
    {
        ScoreboardMode.Unknown => ScoreboardModeModel.Unknown,
        ScoreboardMode.DivisionStatus => ScoreboardModeModel.DivisionStatus,
        ScoreboardMode.CompetitorResults => ScoreboardModeModel.CompetitorResults,
        ScoreboardMode.UpcomingCompetitors => ScoreboardModeModel.UpcomingCompetitors,
        _ => ScoreboardModeModel.Unknown,
    };
}

