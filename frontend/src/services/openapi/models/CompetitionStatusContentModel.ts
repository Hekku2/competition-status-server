/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CurrentCompetitorContentModel } from './CurrentCompetitorContentModel';
import type { DivisionStatusModel } from './DivisionStatusModel';

/**
 * Current status of the competition.
 */
export type CompetitionStatusContentModel = {
    /**
     * Name of the event. Example: National finals 2022
     */
    eventName: string;
    /**
     * Timestamp indicating when this status was generated.
     * This is always In UTC
     * Format "yyyy-MM-ddTHH:mm:ss.fffZ"
     */
    createdAt: string;
    /**
     * Current status of divisions
     */
    divisions: Array<DivisionStatusModel>;
    currentCompetitor?: CurrentCompetitorContentModel;
}
