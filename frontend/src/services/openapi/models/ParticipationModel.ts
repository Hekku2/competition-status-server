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
     * Division name. Should match some division in currently active
     * competition
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
     * If true, competitors are shown as forfeited for this division.
     *
     * In this context, forfeit can happen if:
     * a) Competitor doesn't show up for competition
     * b) Competitor gets injured and is unable to continue
     * c) Competitor is disqualified
     *
     * This doesn't care if it's competitor's fault or not, this just
     * means that competitors score is not shown.
     *
     * This means following:
     * a) Results are not shown, if given.
     * b) These competitors are not shown schedule.
     * c) When listed, competitors are shown in the bottom part of the
     * listing.
     */
    forfeit: boolean;
}
