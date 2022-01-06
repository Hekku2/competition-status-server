using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompetitionController : ControllerBase
    {
        private readonly ILogger<CompetitionController> _logger;

        public CompetitionController(ILogger<CompetitionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("current-competitor")]
        public CurrentCompetitorEnvelopeModel GetCurrentCompetitor()
        {
            return new CurrentCompetitorEnvelopeModel
            {
                Content = new CurrentCompetitorContentModel
                {
                    Division = "test division"
                }
            };
        }
    }
}
