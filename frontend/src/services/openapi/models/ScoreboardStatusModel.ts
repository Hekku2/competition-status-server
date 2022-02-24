/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ParticipationRowModel } from './ParticipationRowModel';
import type { PerformanceResultsContentModel } from './PerformanceResultsContentModel';
import type { ScoreboardModeModel } from './ScoreboardModeModel';
import type { UpcomingCompetitorModel } from './UpcomingCompetitorModel';

export type ScoreboardStatusModel = {
    latestUpdate: string;
    scoreboardMode: ScoreboardModeModel;
    result?: PerformanceResultsContentModel;
    upcomingCompetitors: Array<UpcomingCompetitorModel>;
    results: Array<ParticipationRowModel>;
}
