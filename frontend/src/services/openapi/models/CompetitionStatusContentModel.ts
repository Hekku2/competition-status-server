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
     * Name of the event. Example: "National finals 2022"
     */
    eventName: string;
    /**
     * Timestamp indicating when this status was generated
     */
    createdAt: string;
    /**
     * Current status of divisions
     */
    divisions: Array<DivisionStatusModel>;
    currentCompetitor?: CurrentCompetitorContentModel;
}
