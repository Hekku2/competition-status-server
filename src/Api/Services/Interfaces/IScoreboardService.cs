using System;
using Api.Models;
using DataAccess.Entity;

namespace Api.Services.Interfaces
{
    /// <summary>
    /// Interface to separate scoreboard concerns
    /// </summary>
    public interface IScoreboardService
    {
        /// <summary>
        /// Observable for scoreboard mode
        /// </summary>
        /// <returns>ScoreboardMode enum</returns>
        IObservable<ScoreboardMode> GetScoreboardModeObservable();

        /// <summary>
        /// Current scoreboard mode
        /// </summary>
        /// <returns>Current mode or Unknown</returns>
        ScoreboardMode GetScoreboardMode();

        /// <summary>
        /// Set scoreboard mode. If same as current, this is no-op.
        /// </summary>
        /// <param name="mode">New scoreboard mode</param>
        void SetScoreboardMode(ScoreboardMode mode);

        IObservable<(DivisionEntity, CompetitionOrderEntity)?> GetActiveResultsObservable();
        (DivisionEntity, CompetitionOrderEntity)? GetActiveResults();

        void SetResultsForShowing(int id);
        void SetActiveDivision(string name);

        IObservable<string?> GetActiveDivisionObservable();
    }
}
