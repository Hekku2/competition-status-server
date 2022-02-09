using System;
using System.Linq;
using System.Reactive.Subjects;
using Api.Services.Interfaces;
using DataAccess.Entity;

namespace Api.Services.Implementations
{
    /// <summary>
    /// TODO Make a proper implementation which actually saves the competition entity to database
    /// </summary>
    public class CompetitionService : ICompetitionService, ICompetitionStatusService
    {
        private readonly BehaviorSubject<CurrentCompetitorsEntity?> _currentCompetitor = new(null);

        private CompetitionEntity? _competitionEntity;

        public void UploadCompetition(CompetitionEntity entity)
        {
            _competitionEntity = entity;
            _currentCompetitor.OnNext(_competitionEntity.CurrentCompetitor);
        }

        public CompetitionEntity? GetCurrentState()
        {
            return _competitionEntity;
        }

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

        public void UpdateCurrentCompetitor(int? id)
        {
            var competition = GetCurrentState() ?? throw new InvalidOperationException("No competition set!");
            if (id is null)
            {
                competition.CurrentCompetitor = null;
                UpdateCurrentCompetitor((CurrentCompetitorsEntity?)null);
                return;
            }

            var (division, order) = GetForCurrentCompetitorsEntity(competition, id.Value);
            var entity = new CurrentCompetitorsEntity
            {
                Id = order.Id,
                Division = division.Name,
                Competitors = order.Competitors,
            };
            competition.CurrentCompetitor = entity;
            UpdateCurrentCompetitor(entity);
        }

        public void UpdateResults(int id, PoleDanceResultEntity? results)
        {
            var competition = GetCurrentState() ?? throw new InvalidOperationException("No competition set!");
            foreach (var division in competition.Divisions)
            {
                foreach (var competitionOrder in division.CompetitionOrder)
                {
                    if (competitionOrder.Id == id)
                    {
                        competitionOrder.Result = results;
                    }
                }
            }
        }

        private static (DivisionEntity, CompetitionOrderEntity) GetForCurrentCompetitorsEntity(CompetitionEntity competition, int id)
        {
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
    }
}
