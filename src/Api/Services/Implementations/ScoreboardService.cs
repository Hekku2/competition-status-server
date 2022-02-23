using System;
using System.Reactive.Subjects;
using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations;

public class ScoreboardService : IScoreboardService
{
    private readonly BehaviorSubject<ScoreboardMode> _currentScoreboardMode = new(ScoreboardMode.Unknown);
    private readonly BehaviorSubject<(DivisionEntity, CompetitionOrderEntity)?> _results = new(null);
    private readonly ICompetitionService _competitionService;

    public ScoreboardService(ICompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    public IObservable<ScoreboardMode> GetScoreboardModeObservable()
    {
        return _currentScoreboardMode;
    }

    public IObservable<(DivisionEntity, CompetitionOrderEntity)?> GetActiveResults()
    {
        return _results;
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
}
