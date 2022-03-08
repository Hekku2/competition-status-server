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
        /// Returns all reported results in order they were reported.
        /// </summary>
        /// <returns>Array of results in report order</returns>
        PerformanceResultsEntity[] GetReportedResults();

        /// <summary>
        /// Current competitors and series, or null.
        /// </summary>
        /// <returns>Current competitor, or null.</returns>
        CurrentCompetitorsEntity? GetCurrentCompetitor();

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

        IObservable<DateTime> GetLatestUpdateTime();
    }
}
