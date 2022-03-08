using System;
using System.Linq;
using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations;

public class CompetitionDataAccess : ICompetitionDataAccess
{
    private CompetitionEntity? _competitionEntity;

    public CompetitionEntity? GetCurrentState()
    {
        return _competitionEntity;
    }

    public void UpdateState(CompetitionEntity entity)
    {
        _competitionEntity = entity;
    }

    public (DivisionEntity, CompetitionOrderEntity) GetForCurrentCompetitorsEntity(int id)
    {
        var competition = GetCurrentState() ?? throw new InvalidOperationException("No competition set!");
        var matches =
            from division in competition.Divisions
            from order in division.CompetitionOrder
            where order.Id == id
            select new
            {
                Division = division,
                CompetitionOrder = order
            };

        var match = matches.First();
        return (match.Division, match.CompetitionOrder);
    }

    public DivisionEntity? GetDivisionEntity(string divisionName)
    {
        var competition = GetCurrentState() ?? throw new InvalidOperationException("No competition set!");
        return competition.Divisions.FirstOrDefault(division => division.Name == divisionName);
    }
}
