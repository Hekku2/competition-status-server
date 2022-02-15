/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorModel } from './CompetitorModel';
import type { PoleSportResultModel } from './PoleSportResultModel';

/**
 * Describes a result for competitor(s) in for a single performance and what
 * place it did achieve, if any.
 */
export type PerformanceResultsContentModel = {
    /**
     * Name of the division. Should match some division in currenlty active
     * competition.
     */
    division: string;
    /**
     * Placement that the competitor(s) received with this result. This is null
     * If current place couldn't be calculated.
     * Starts from 1.
     */
    currentPlace?: number | null;
    /**
     * Competitors that did the performance. This should contain at least one
     * item.
     */
    competitors: Array<CompetitorModel>;
    result: PoleSportResultModel;
}
