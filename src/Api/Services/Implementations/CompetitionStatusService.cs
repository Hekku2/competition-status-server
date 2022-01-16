using System;
using System.Reactive.Subjects;
using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations
{
    public class CompetitionStatusService : ICompetitionStatusService
    {
        private readonly BehaviorSubject<CurrentCompetitorsEntity?> _currentCompetitor = new(null);

        public CurrentCompetitorsEntity? GetCurrentCompetitor()
        {
            return _currentCompetitor.Value;
        }

        public IObservable<CurrentCompetitorsEntity?> GetCurrentCompetitorObservable()
        {
            return _currentCompetitor;
        }

        public void UpdateCurrentCompetitor(CurrentCompetitorsEntity? competitor)
        {
            _currentCompetitor.OnNext(competitor);
        }
    }
}
