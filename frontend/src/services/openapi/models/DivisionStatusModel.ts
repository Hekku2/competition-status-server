/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ResultRowModel } from './ResultRowModel';
import type { UpcomingCompetitorModel } from './UpcomingCompetitorModel';

/**
 * This represens a current status for a division.
 * This is generated on the fly.
 */
export type DivisionStatusModel = {
    /**
     * Name if the division. Example: "Senior women"
     */
    name: string;
    /**
     * Current results in order. Forfeited are not yet listed
     */
    results: Array<ResultRowModel>;
    /**
     * Forfeited competitors. Is empty if no one has forfeited.
     */
    forfeited: Array<ResultRowModel>;
    /**
     * Upcoming competitors. First is in zero index.
     * This can be empty, if no competitors are remaining.
     */
    upcomingCompetitorModels: Array<UpcomingCompetitorModel>;
}
