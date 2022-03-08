using System.Linq;
using Api.Models;
using Api.Services.Interfaces;
using Api.Util;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScoreboardController : ControllerBase
    {
        private readonly IScoreboardService _scoreboardService;

        public ScoreboardController(IScoreboardService scoreboardService)
        {
            _scoreboardService = scoreboardService;
        }

        [HttpGet]
        [Route("")]
        public ScoreboardStatusModel GetStatus()
        {
            var current = _scoreboardService.GetScoreboardMode();
            var activeResult = _scoreboardService.GetActiveResults();
            return new ScoreboardStatusModel
            {
                ScoreboardMode = current.ToScoreboardModeModel(),
                Result = activeResult != null ? CreatePerformanceResultsContentModel(activeResult) : null
            };
        }

        [HttpPut]
        [Route("set-mode")]
        public void SetScoreboardMode(ScoreboardModeModel mode)
        {
            _scoreboardService.SetScoreboardMode(mode.ToScoreboardMode());
        }

        /// <summary>
        /// Sets results that will be shown. Doesn't show the results yet.
        /// This is done with "set-mode"
        /// </summary>
        /// <param name="id">id</param>
        [HttpPut]
        [Route("select-results")]
        public void SelectResultForShowing(int id)
        {
            _scoreboardService.SetResultsForShowing(id);
        }

        [HttpPut]
        [Route("set-active-division")]
        public void SetActiveDivision(string name)
        {
            _scoreboardService.SetActiveDivision(name);
        }

        private static PerformanceResultsContentModel? CreatePerformanceResultsContentModel((DivisionEntity, CompetitionOrderEntity)? resultEntity)
        {
            if (resultEntity == null)
                return null;

            return new PerformanceResultsContentModel
            {
                Competitors = resultEntity.Value.Item2.Competitors.Select(EntityMappingExtensions.ToCompetitorModel).ToArray(),
                Division = resultEntity.Value.Item1.Name,
                CurrentPlace = CompetitionOrderUtil.CalculatePlacement(resultEntity.Value.Item1.CompetitionOrder, resultEntity.Value.Item2.Id),
                Result = resultEntity.Value.Item2.Result?.ToPoleSportResultModel() ?? new PoleSportResultModel()
            };
        }
    }
}
