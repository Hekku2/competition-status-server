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
     * Division of competitor(s). Example: "Senior Women"
     */
    division: string;
    /**
     * Competitor(s)
     */
    competitors: Array<CompetitorModel>;
}
