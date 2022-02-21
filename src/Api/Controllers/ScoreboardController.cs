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
    }
}
