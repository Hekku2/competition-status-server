using Api.Models;
using Api.Services.Interfaces;
using Api.Util;
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
            return new ScoreboardStatusModel
            {
                ScoreboardMode = current.ToScoreboardModeModel()
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
    }
}
