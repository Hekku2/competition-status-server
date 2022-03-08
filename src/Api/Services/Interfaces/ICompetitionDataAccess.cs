using DataAccess.Entity;

namespace Api.Services.Interfaces;

public interface ICompetitionDataAccess
{
    CompetitionEntity? GetCurrentState();
    void UpdateState(CompetitionEntity entity);
    (DivisionEntity, CompetitionOrderEntity) GetForCurrentCompetitorsEntity(int id);

    DivisionEntity? GetDivisionEntity(string divisionName);
}
