using System;
using System.Reactive.Subjects;
using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations;

public class ScoreboardService : IScoreboardService
{
    private readonly BehaviorSubject<ScoreboardMode> _currentScoreboardMode = new(ScoreboardMode.Unknown);

    public IObservable<ScoreboardMode> GetScoreboardModeObservable()
    {
        return _currentScoreboardMode;
    }

    public ScoreboardMode GetScoreboardMode()
    {
        return _currentScoreboardMode.Value;
    }

    public void SetScoreboardMode(ScoreboardMode mode)
    {
        _currentScoreboardMode.OnNext(mode);
    }
}
