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

        public CompetitionController(ILogger<CompetitionController> logger, ICompetitionStatusService competitionStatusService)
        {
            _logger = logger;
            _competitionStatusService = competitionStatusService;
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

        private static CurrentCompetitorContentModel CreateCurrentCompetitorContentModel(CurrentCompetitorsEntity entity)
        {
            return new CurrentCompetitorContentModel
            {
                Division = "test division",
                Attempt = "1",
                Competitors = entity.Competitors.Select(CreateCompetitorModel).ToArray()
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
