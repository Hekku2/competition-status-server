using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Subjects;
using Api.Services.Interfaces;
using Api.Util;
using DataAccess.Entity;

namespace Api.Services.Implementations;

/// <summary>
/// TODO Make a proper implementation which actually saves the competition entity to database
/// </summary>
public class CompetitionService : ICompetitionService, ICompetitionStatusService
{
    private readonly BehaviorSubject<CurrentCompetitorsEntity?> _currentCompetitor = new(null);
    private readonly Subject<PerformanceResultsEntity> _performanceResults = new();

    private readonly ConcurrentQueue<PerformanceResultsEntity> _concurrentQueue = new();
    private readonly ICompetitionDataAccess _competitionDataAccess;

    private readonly BehaviorSubject<DateTime> _latestUpdate = new(DateTime.UtcNow);

    public CompetitionService(ICompetitionDataAccess competitionDataAccess)
    {
        _competitionDataAccess = competitionDataAccess;
    }

    public void UploadCompetition(CompetitionEntity entity)
    {
        _competitionDataAccess.UpdateState(entity);
        _currentCompetitor.OnNext(entity.CurrentCompetitor);
        _latestUpdate.OnNext(DateTime.UtcNow);
    }

    public CurrentCompetitorsEntity? GetCurrentCompetitor()
    {
        return _currentCompetitor.Value;
    }

    public PerformanceResultsEntity[] GetReportedResults()
    {
        return _concurrentQueue.ToArray();
    }

    public IObservable<PerformanceResultsEntity> GetPerformanceResultsObservable()
    {
        return _performanceResults;
    }

    public IObservable<CurrentCompetitorsEntity?> GetCurrentCompetitorObservable()
    {
        return _currentCompetitor;
    }

    public IObservable<DateTime> GetLatestUpdateTime()
    {
        return _latestUpdate;
    }

    public void UpdateCurrentCompetitor(int? id)
    {
        var competition = _competitionDataAccess.GetCurrentState() ?? throw new InvalidOperationException("No competition set!");
        if (id is null)
        {
            competition.CurrentCompetitor = null;
            _currentCompetitor.OnNext(null);
            return;
        }

        var (division, order) = _competitionDataAccess.GetForCurrentCompetitorsEntity(id.Value);
        var entity = new CurrentCompetitorsEntity
        {
            Id = order.Id,
            Division = division.Name,
            Competitors = order.Competitors,
        };
        competition.CurrentCompetitor = entity;
        _currentCompetitor.OnNext(entity);
        _latestUpdate.OnNext(DateTime.UtcNow);
    }

    public void UpdateResults(int id, PoleDanceResultEntity? results)
    {
        var competition = _competitionDataAccess.GetCurrentState() ?? throw new InvalidOperationException("No competition set!");
        foreach (var division in competition.Divisions)
        {
            foreach (var competitionOrder in division.CompetitionOrder)
            {
                if (competitionOrder.Id == id)
                {
                    competitionOrder.Result = results;

                    if (results is not null)
                    {
                        var entity = new PerformanceResultsEntity
                        {
                            Division = division.Name,
                            Result = results,
                            Competitors = competitionOrder.Competitors,
                            CurrentPlace = CompetitionOrderUtil.CalculatePlacement(division.CompetitionOrder, id)
                        };
                        _concurrentQueue.Enqueue(entity);
                        _performanceResults.OnNext(entity);
                    }
                }
            }
        }
        _latestUpdate.OnNext(DateTime.UtcNow);
    }
}
