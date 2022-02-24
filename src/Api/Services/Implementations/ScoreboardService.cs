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

    private readonly BehaviorSubject<string?> _activeDivision = new(null);
    private readonly ICompetitionDataAccess _competitionDataAccess;

    public ScoreboardService(ICompetitionDataAccess competitionDataAccess)
    {
        _competitionDataAccess = competitionDataAccess;
    }

    public IObservable<ScoreboardMode> GetScoreboardModeObservable()
    {
        return _currentScoreboardMode;
    }

    public IObservable<string?> GetActiveDivisionObservable()
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
        var result = _competitionDataAccess.GetForCurrentCompetitorsEntity(id);
        _results.OnNext(result);
    }

    public void SetActiveDivision(string name)
    {
        _activeDivision.OnNext(name);
    }
}
