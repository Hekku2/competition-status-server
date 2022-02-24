using System;
using System.Linq;
using Api.Models;
using Api.Services.Interfaces;
using Api.Util;
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
        private readonly ICompetitionDataAccess _competitionDataAccess;

        public CompetitionController(ILogger<CompetitionController> logger, ICompetitionStatusService competitionStatusService, ICompetitionService competitionService, ICompetitionDataAccess competitionDataAccess)
        {
            _logger = logger;
            _competitionStatusService = competitionStatusService;
            _competitionService = competitionService;
            _competitionDataAccess = competitionDataAccess;
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
                Content = current?.ToCurrentCompetitorContentModel()
            };
        }

        /// <summary>
        /// Returns history of reported results. This is mainly used for debugging purposes
        /// and should not be used for reporting.
        /// </summary>
        /// <returns>List of of results in report order.</returns>
        [HttpGet]
        [Route("result-history")]
        public PerformanceResultsEnvelopeModel[] GetResults()
        {
            return _competitionStatusService.GetReportedResults().Select(CreatePerformanceResultsEnvelopeModel).ToArray();
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

        /// <summary>
        /// Set result for competitor
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("set-result")]
        public void SetResult(CompetitorResultModel model)
        {
            _logger.LogInformation("Setting results fro comeptitor #{Id}", model.Id);
            _competitionStatusService.UpdateResults(model.Id, model.Results?.ToPoleDanceResultEntity());
        }

        /// <summary>
        /// Upload compettion data. This overrides all data
        /// </summary>
        /// <param name="fileModel">model representing the json file</param>
        [HttpPost]
        [Route("upload-competition")]
        public void UploadCompetition(CompetitionFileModel fileModel)
        {
            _logger.LogInformation("Competition data uploaded");
            var entity = new CompetitionEntity
            {
                Name = fileModel.Name,
                Divisions = fileModel.Divisions.Select(ModelMappingExtensions.ToDivisionEntity).ToArray(),
                CurrentCompetitor = fileModel.CurrentCompetitor?.ToCurrentCompetitorsEntity()
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
            var entity = _competitionDataAccess.GetCurrentState();
            return new CompetitionStatusEnvelopeModel
            {
                Content = entity?.ToCompetitionStatusContentModel()
            };
        }

        /// <summary>
        /// Returns competition status in file model.
        /// </summary>
        /// <returns>CompetitionFileModel</returns>
        [HttpGet]
        [Route("download-competition")]
        public CompetitionFileModel DownloadCompetition()
        {
            var entity = _competitionDataAccess.GetCurrentState() ?? throw new InvalidOperationException("No competition in progress");
            return entity.ToCompetitionFileModel();
        }

        private PerformanceResultsEnvelopeModel CreatePerformanceResultsEnvelopeModel(PerformanceResultsEntity entity)
        {
            return new PerformanceResultsEnvelopeModel
            {
                Content = entity.ToPerformanceResultsContentModel()
            };
        }
    }
}
