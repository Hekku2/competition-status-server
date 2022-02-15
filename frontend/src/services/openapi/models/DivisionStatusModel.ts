/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ParticipationRowModel } from './ParticipationRowModel';
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
    results: Array<ParticipationRowModel>;
    /**
     * Forfeited competitors. Is empty if no one has forfeited. These are
     * not returned in any special order.
     */
    forfeited: Array<ParticipationRowModel>;
    /**
     * Upcoming competitors. First is in zero index.
     * This can be empty, if no competitors are remaining.
     */
    upcomingCompetitorModels: Array<UpcomingCompetitorModel>;
}
