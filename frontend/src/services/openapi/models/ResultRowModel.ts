/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorModel } from './CompetitorModel';
import type { PoleSportResultModel } from './PoleSportResultModel';

/**
 * This represents a finished or or otherwise resolved performance result.
 * Result might be missing, if performance has been finished but has not been
 * graded yet or if the competitor has forfeited.
 */
export type ResultRowModel = {
    /**
     * Unique ID for these comeptitors
     */
    id: number;
    /**
     * Competitors
     */
    competitors: Array<CompetitorModel>;
    result?: PoleSportResultModel;
    /**
     * If true, this competitor has forfeited and should not have a result
     */
    forfeit: boolean;
}
