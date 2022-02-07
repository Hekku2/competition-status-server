using Api.Services.Interfaces;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ICompetitionStatusService _competitionStatusService;

        public TestController(ILogger<TestController> logger, ICompetitionStatusService competitionStatusService)
        {
            _logger = logger;
            _competitionStatusService = competitionStatusService;
        }

        [HttpPost]
        [Route("set-current-competitors")]
        public void LoadPoleCompetitionData(CurrentCompetitorsEntity entity)
        {
            _logger.LogWarning("Preparing to set competitor...");
            _competitionStatusService.UpdateCurrentCompetitor(entity);
            _logger.LogWarning("Competitor set!");
        }
    }
}
