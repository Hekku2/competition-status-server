using System;
using DataAccess.Entity;

namespace Api.Services.Interfaces
{
    public interface ICompetitionStatusService
    {
        /// <summary>
        /// Observable for currently performing competitor. May return null
        /// if no one is performing currently
        /// </summary>
        /// <returns>Observable of current performer</returns>
        IObservable<CurrentCompetitorsEntity?> GetCurrentCompetitorObservable();

        /// <summary>
        /// Returns peformance results.
        /// </summary>
        /// <returns>Observable of performance results</returns>
        IObservable<PerformanceResultsEntity> GetPerformanceResultsObservable();

        /// <summary>
        /// Current competitors and series, or null.
        /// </summary>
        /// <returns>Current competitor, or null.</returns>
        CurrentCompetitorsEntity? GetCurrentCompetitor();

        /// <summary>
        /// Sets current competitors. Should only be used to override database.
        /// </summary>
        /// <param name="competitor">Current competitors</param>
        void UpdateCurrentCompetitor(CurrentCompetitorsEntity? competitor);

        /// <summary>
        /// Sets current competitor
        /// </summary>
        /// <param name="id">ID of current competitor</param>
        void UpdateCurrentCompetitor(int? id);

        /// <summary>
        /// Updates results for given competitor
        /// </summary>
        /// <param name="id">ID of comeptitor</param>
        /// <param name="results">Results. If null, results are removed.</param>
        void UpdateResults(int id, PoleDanceResultEntity? results);
    }
}
