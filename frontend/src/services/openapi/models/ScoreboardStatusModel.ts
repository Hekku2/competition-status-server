/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { PerformanceResultsContentModel } from './PerformanceResultsContentModel';
import type { ScoreboardModeModel } from './ScoreboardModeModel';

export type ScoreboardStatusModel = {
    scoreboardMode: ScoreboardModeModel;
    result?: PerformanceResultsContentModel;
}
