using System;
using System.Linq;
using System.Reactive.Subjects;
using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations;

public class ScoreboardService : IScoreboardService
{
    private readonly BehaviorSubject<ScoreboardMode> _currentScoreboardMode = new(ScoreboardMode.Unknown);
    private readonly BehaviorSubject<(DivisionEntity, CompetitionOrderEntity)?> _results = new(null);

    private readonly BehaviorSubject<DivisionEntity?> _activeDivision = new(null);
    private readonly ICompetitionService _competitionService;

    public ScoreboardService(ICompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    public IObservable<ScoreboardMode> GetScoreboardModeObservable()
    {
        return _currentScoreboardMode;
    }

    public IObservable<DivisionEntity?> GetActiveDivisionObservable()
    {
        return _activeDivision;
    }

    public IObservable<(DivisionEntity, CompetitionOrderEntity)?> GetActiveResultsObservable()
    {
        return _results;
    }

    public (DivisionEntity, CompetitionOrderEntity)? GetActiveResults()
    {
        return _results.Value;
    }


    public ScoreboardMode GetScoreboardMode()
    {
        return _currentScoreboardMode.Value;
    }

    public void SetScoreboardMode(ScoreboardMode mode)
    {
        _currentScoreboardMode.OnNext(mode);
    }

    public void SetResultsForShowing(int id)
    {
        var result = _competitionService.GetForCurrentCompetitorsEntity(id);
        _results.OnNext(result);
    }

    public void SetActiveDivision(string name)
    {
        var division = _competitionService.GetCurrentState()?.Divisions.FirstOrDefault(division => division.Name == name);
        _activeDivision.OnNext(division);
    }
}
