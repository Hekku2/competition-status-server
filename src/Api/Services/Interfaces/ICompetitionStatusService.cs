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
        /// Current competitors and series, or null.
        /// </summary>
        /// <returns>Current competitor, or null.</returns>
        CurrentCompetitorsEntity? GetCurrentCompetitor();

        /// <summary>
        /// Sets current competitors
        /// </summary>
        /// <param name="competitor">Current competitors</param>
        void UpdateCurrentCompetitor(CurrentCompetitorsEntity? competitor);
    }
}
