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

        /// <summary>
        /// Current competitor information
        /// </summary>
        /// <returns>CurrentCompetitorEnvelopeModel</returns>
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

        /// <summary>
        /// Sets current competitor or remove current competitor.
        /// </summary>
        /// <param name="model">Competitor ID</param>
        [HttpPost]
        [Route("set-current-competitor")]
        public void SetCurrentCompetitor(CurrentCompetitorSetModel model)
        {
            _logger.LogInformation("Current competitor set to #{Id}", model.Id);
            _competitionStatusService.UpdateCurrentCompetitor(model.Id);
        }

        [HttpPost]
        [Route("upload-competition")]
        public void UploadCompetition(CompetitionFileModel fileModel)
        {
            _logger.LogInformation("Competition data uploaded");
            var entity = new CompetitionEntity
            {
                Name = fileModel.Name,
                Divisions = fileModel.Divisions.Select(CreateDivisionEntity).ToArray(),
                CurrentCompetitor = fileModel.CurrentCompetitor != null ? CreateCurrentCompetitorsEntity(fileModel.CurrentCompetitor) : null
            };
            _competitionService.UploadCompetition(entity);
        }

        /// <summary>
        /// Returns current status for the whole competition
        /// </summary>
        /// <returns>CompetitionStatusEnvelopeModel</returns>
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

        private static CurrentCompetitorsEntity CreateCurrentCompetitorsEntity(CurrentCompetitorFileModel model)
        {
            return new CurrentCompetitorsEntity
            {
                Competitors = model.Competitors.Select(CreateCompetitorEntity).ToArray(),
                Division = model.Division,
                Id = model.Id
            };
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
                Id = model.Id,
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

        private static CompetitionStatusContentModel CreateCompetitionStatusContentModel(CompetitionEntity entity)
        {
            return new CompetitionStatusContentModel
            {
                EventName = entity.Name,
                CreatedAt = DateTime.UtcNow.ToString(),
                Divisions = entity.Divisions.Select(CreateDivisionStatusModel).ToArray(),
                CurrentCompetitor = entity.CurrentCompetitor != null ? CreateCurrentCompetitorContentModel(entity.CurrentCompetitor) : null
            };
        }

        private static CurrentCompetitorContentModel CreateCurrentCompetitorContentModel(CurrentCompetitorsEntity entity)
        {
            return new CurrentCompetitorContentModel
            {
                Division = entity.Division,
                Competitors = entity.Competitors.Select(CreateCompetitorModel).ToArray()
            };
        }

        private static DivisionStatusModel CreateDivisionStatusModel(DivisionEntity entity)
        {
            var results = entity
                .CompetitionOrder
                .Where(item => item.Forfeit || item.Result is not null)
                .Select(CreateResultRowModel)
                .OrderByDescending(item => item.Forfeit ? -9999 : item.Result?.Total)
                .ToArray();

            var upcoming = entity
                .CompetitionOrder
                .Where(item => !item.Forfeit && item.Result is null)
                .Select(CreateUpcomingCompetitorModel)
                .ToArray();

            return new DivisionStatusModel
            {
                Name = entity.Name,
                Results = results,
                UpcomingCompetitorModels = upcoming
            };
        }

        private static UpcomingCompetitorModel CreateUpcomingCompetitorModel(CompetitionOrderEntity entity)
        {
            return new UpcomingCompetitorModel
            {
                Id = entity.Id,
                Competitors = entity.Competitors.Select(CreateCompetitorModel).ToArray()
            };
        }

        private static ResultRowModel CreateResultRowModel(CompetitionOrderEntity entity)
        {
            return new ResultRowModel
            {
                Id = entity.Id,
                Competitors = entity.Competitors.Select(CreateCompetitorModel).ToArray(),
                Result = entity.Result != null && !entity.Forfeit ? CreatePoleDanceResultEntity(entity.Result) : null,
                Forfeit = entity.Forfeit
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
                Name = entity.Name,
                Team = entity.Team
            };
        }
    }
}
