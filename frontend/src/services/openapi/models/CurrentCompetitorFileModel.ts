/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorFileModel } from './CompetitorFileModel';

/**
 * Represents file model of current competitor performing or performing next
 * when no other competitor is not active.
 */
export type CurrentCompetitorFileModel = {
    /**
     * ID of competitor. This might not be set if competitor is not listed.
     */
    id?: number | null;
    /**
     * Division of competitor(s). This should match some division in
     * current competition.
     */
    division: string;
    /**
     * Competitor(s). This should have at least one value, but may have
     * multiple values if there are multiple persons performing for single
     * performance.
     */
    competitors: Array<CompetitorFileModel>;
}
