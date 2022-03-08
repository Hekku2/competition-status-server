namespace Api.Models;

/// <summary>
/// This desribes the mode that the scoreboard should be in.
/// </summary>
public enum ScoreboardModeModel
{
    /// <summary>
    /// This should not be used. This is an failsafe for serialization error
    /// </summary>
    Unknown,

    /// <summary>
    /// Shows current status of division (scores and placements).
    /// </summary>
    DivisionStatus,

    /// <summary>
    /// Shows results for a single performance.
    /// </summary>
    CompetitorResults,

    /// <summary>
    /// Shows upcoming competitors in order.
    /// </summary>
    UpcomingCompetitors
}
