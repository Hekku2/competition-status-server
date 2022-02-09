using System.Linq;
using Api.Models;
using DataAccess.Entity;

namespace Api.Controllers
{
    public static class ModelMappingExtensions
    {
        public static CurrentCompetitorsEntity ToCurrentCompetitorsEntity(this CurrentCompetitorFileModel model) => new()
        {
            Competitors = model.Competitors.Select(ToCompetitorEntity).ToArray(),
            Division = model.Division,
            Id = model.Id
        };

        public static DivisionEntity ToDivisionEntity(this DivisionFileModel model) => new()
        {
            Name = model.Name,
            CompetitionOrder = model.Items.Select(ToCompetitionOrderEntity).ToArray()
        };

        public static CompetitionOrderEntity ToCompetitionOrderEntity(this CompetitorPositionFileModel model)
        {
            return new CompetitionOrderEntity
            {
                Id = model.Id,
                Competitors = model.Competitors.Select(ToCompetitorEntity).ToArray(),
                Forfeit = model.Forfeit,
                Result = model.Results?.ToPoleDanceResultEntity()
            };
        }

        public static PoleDanceResultEntity ToPoleDanceResultEntity(this PoleResultFileModel model) => new()
        {
            ArtisticScore = model.ArtisticScore,
            DifficultyScore = model.DifficultyScore,
            ExecutionScore = model.ExecutionScore,
            HeadJudgePenalty = model.HeadJudgePenalty
        };

        public static CompetitorEntity ToCompetitorEntity(this CompetitorFileModel model) => new()
        {
            Name = model.Name,
            Team = model.Team
        };
    }
}
