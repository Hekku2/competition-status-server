using System;
using System.Linq;
using Api.Models;
using Api.Services.Interfaces;
using Api.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers;

/// <summary>
/// Controller for competitor management
/// </summary>
[ApiController]
[Route("[controller]")]
public class CompetitorController : ControllerBase
{
    private readonly ILogger<CompetitorController> _logger;
    private readonly ICompetitionDataAccess _competitionDataAccess;

    public CompetitorController(ILogger<CompetitorController> logger, ICompetitionDataAccess competitionDataAccess)
    {
        _logger = logger;
        _competitionDataAccess = competitionDataAccess;
    }

    [HttpGet]
    [Route("all")]
    public ParticipationModel[] GetAll()
    {
        var state = _competitionDataAccess.GetCurrentState();
        if (state is null)
        {
            return Array.Empty<ParticipationModel>();
        }

        var all = from division in state.Divisions
                  from participation in division.CompetitionOrder
                  select new ParticipationModel
                  {
                      Id = participation.Id,
                      Division = division.Name,
                      Forfeit = participation.Forfeit,
                      Competitors = participation.Competitors.Select(EntityMappingExtensions.ToCompetitorModel).ToArray(),
                      Result = participation.Result?.ToPoleSportResultModel()
                  };
        return all.ToArray();
    }
}
