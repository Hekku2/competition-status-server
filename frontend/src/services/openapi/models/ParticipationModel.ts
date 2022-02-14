/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorModel } from './CompetitorModel';
import type { PoleSportResultModel } from './PoleSportResultModel';

/**
 * Represents a participation in division by one or multiple competitors.
 */
export type ParticipationModel = {
    /**
     * Division name
     */
    division: string;
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
