using System;
using System.Linq;
using Api.Models;
using Api.Services.Interfaces;
using DataAccess.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DivisionController : ControllerBase
{
    private readonly ILogger<CompetitorController> _logger;
    private readonly ICompetitionDataAccess _competitionDataAccess;

    public DivisionController(ILogger<CompetitorController> logger, ICompetitionDataAccess competitionDataAccess)
    {
        _logger = logger;
        _competitionDataAccess = competitionDataAccess;
    }

    [HttpGet]
    [Route("all")]
    public DivisionListModel[] GetAll()
    {
        var state = _competitionDataAccess.GetCurrentState();
        if (state is null)
        {
            return Array.Empty<DivisionListModel>();
        }

        return state.Divisions.Select(CreateDivisionListModel).ToArray();
    }

    private static DivisionListModel CreateDivisionListModel(DivisionEntity entity)
    {
        return new DivisionListModel
        {
            Name = entity.Name
        };
    }
}
