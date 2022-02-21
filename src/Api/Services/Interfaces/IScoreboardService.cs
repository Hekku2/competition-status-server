using System;
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
    }
}
