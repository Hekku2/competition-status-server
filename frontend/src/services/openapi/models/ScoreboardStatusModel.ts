/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { PerformanceResultsContentModel } from './PerformanceResultsContentModel';
import type { ScoreboardModeModel } from './ScoreboardModeModel';
import type { UpcomingCompetitorModel } from './UpcomingCompetitorModel';

export type ScoreboardStatusModel = {
    scoreboardMode: ScoreboardModeModel;
    result?: PerformanceResultsContentModel;
    upcomingCompetitors: Array<UpcomingCompetitorModel>;
}
