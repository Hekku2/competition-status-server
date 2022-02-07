using System;
using System.Linq;
using Api.Models;
using Api.Services.Interfaces;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly ILogger<CompetitionController> _logger;
        private readonly ICompetitionStatusService _competitionStatusService;
        private readonly ICompetitionService _competitionService;

        public CompetitionController(ILogger<CompetitionController> logger, ICompetitionStatusService competitionStatusService, ICompetitionService competitionService)
        {
            _logger = logger;
            _competitionStatusService = competitionStatusService;
            _competitionService = competitionService;
        }

        [HttpGet]
        [Route("current-competitor")]
        public CurrentCompetitorEnvelopeModel GetCurrentCompetitor()
        {
            var current = _competitionStatusService.GetCurrentCompetitor();
            return new CurrentCompetitorEnvelopeModel
            {
                Content = current != null ? CreateCurrentCompetitorContentModel(current) : null
            };
        }

        [HttpPost]
        [Route("upload-competition")]
        public void UploadCompetition(CompetitionFileModel fileModel)
        {
            // Create competition from given model
            var entity = new CompetitionEntity
            {
                Name = fileModel.Name,
                Divisions = fileModel.Divisions.Select(CreateDivisionEntity).ToArray()
            };
            _competitionService.UploadCompetition(entity);
        }

        private DivisionEntity CreateDivisionEntity(DivisionFileModel model)
        {
            return new DivisionEntity()
            {
                Name = model.Name,
                CompetitionOrder = model.Items.Select(CreateCompetitionOrderEntity).ToArray()
            };
        }

        private CompetitionOrderEntity CreateCompetitionOrderEntity(CompetitorPositionFileModel model)
        {
            return new CompetitionOrderEntity
            {
                Competitors = model.Competitors.Select(CreateCompetitorEntity).ToArray(),
                Forfeit = model.Forfeit,
                Result = model.Results != null ? CreatePoleDanceResultEntity(model.Results) : null
            };
        }

        private static PoleDanceResultEntity CreatePoleDanceResultEntity(PoleResultFileModel model)
        {
            return new PoleDanceResultEntity
            {
                ArtisticScore = model.ArtisticScore,
                DifficultyScore = model.DifficultyScore,
                ExecutionScore = model.ExecutionScore,
                HeadJudgePenalty = model.HeadJudgePenalty
            };
        }

        private static CompetitorEntity CreateCompetitorEntity(CompetitorFileModel model)
        {
            return new CompetitorEntity
            {
                Name = model.Name,
                Team = model.Team
            };
        }

        [HttpGet]
        [Route("competition-status")]
        public CompetitionStatusEnvelopeModel GetCompetitionStatus()
        {
            var entity = _competitionService.GetCurrentState();
            return new CompetitionStatusEnvelopeModel
            {
                Content = entity != null ? CreateCompetitionStatusContentModel(entity) : null
            };
        }

        private static CompetitionStatusContentModel CreateCompetitionStatusContentModel(CompetitionEntity entity)
        {
            return new CompetitionStatusContentModel
            {
                EventName = entity.Name,
                CreatedAt = DateTime.UtcNow.ToString(),
                Divisions = entity.Divisions.Select(CreateDivisionStatusModel).ToArray()
            };
        }


        private static CurrentCompetitorContentModel CreateCurrentCompetitorContentModel(CurrentCompetitorsEntity entity)
        {
            return new CurrentCompetitorContentModel
            {
                Division = "test division",
                Attempt = "1",
                Competitors = entity.Competitors.Select(CreateCompetitorModel).ToArray(),
                PreviousResults = Array.Empty<object>()
            };
        }

        private static DivisionStatusModel CreateDivisionStatusModel(DivisionEntity entity)
        {
            var results = entity
                .CompetitionOrder
                .Where(item => !item.Forfeit && item.Result is not null)
                .Select(CreateResultRowModel)
                .OrderByDescending(item => item.Result?.Total)
                .ToArray();

            return new DivisionStatusModel
            {
                Name = entity.Name,
                Results = results
            };
        }

        private static ResultRowModel CreateResultRowModel(CompetitionOrderEntity resultRow)
        {
            return new ResultRowModel
            {
                Competitors = resultRow.Competitors.Select(CreateCompetitorModel).ToArray(),
                Result = resultRow.Result != null ? CreatePoleDanceResultEntity(resultRow.Result) : null
            };
        }

        private static PoleSportResultModel CreatePoleDanceResultEntity(PoleDanceResultEntity model)
        {
            return new PoleSportResultModel
            {
                ArtisticScore = model.ArtisticScore,
                DifficultyScore = model.DifficultyScore,
                ExecutionScore = model.ExecutionScore,
                HeadJudgePenalty = model.HeadJudgePenalty,
                Total = model.ArtisticScore + model.DifficultyScore + model.ExecutionScore - model.HeadJudgePenalty
            };
        }

        private static CompetitorModel CreateCompetitorModel(CompetitorEntity entity)
        {
            return new CompetitorModel
            {
                CompetitorId = entity.CompetitorId,
                Name = entity.Name,
                Team = entity.Team
            };
        }
    }
}
