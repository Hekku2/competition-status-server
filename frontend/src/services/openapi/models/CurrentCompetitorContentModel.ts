/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorModel } from './CompetitorModel';

/**
 * Represents current competitor who is performing or performing next
 * when no other competitor is not active.
 */
export type CurrentCompetitorContentModel = {
    /**
     * Division of competitor(s). This should match some division in active
     * competition
     */
    division: string;
    /**
     * Competitor(s). This should have at least one value, but may have
     * multiple values if there are multiple persons performing for single
     * performance.
     */
    competitors: Array<CompetitorModel>;
}
