/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorModel } from './CompetitorModel';

/**
 * Represents an upcoming competitor.
 */
export type UpcomingCompetitorModel = {
    /**
     * Unique ID for these competitors
     */
    id: number;
    /**
     * Competitor(s). Contains at least one entity.
     */
    competitors: Array<CompetitorModel>;
}
