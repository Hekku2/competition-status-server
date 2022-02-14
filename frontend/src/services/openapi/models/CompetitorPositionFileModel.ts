/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorFileModel } from './CompetitorFileModel';
import type { PoleResultFileModel } from './PoleResultFileModel';

/**
 * Represents a competitor or competitors (if doubles or other team
 * activity) who participate in division. This class describes what is the
 * status of their participation in the division.
 */
export type CompetitorPositionFileModel = {
    /**
     * Unique ID for these competitors. This is asystem ID, which is used
     * to set the competitor(s) as active competitor.
     * NOTE: This is not the same as a jersey number etc.
     */
    id: number;
    /**
     * Competitors, should have at least one entity.
     */
    competitors: Array<CompetitorFileModel>;
    /**
     * If true, competitors are shown as forfeited for this division.
     * This means following:
     * a) Results are not shown, if given.
     * b) These competitors are not shown schedule.
     * c) When listed, competitors are shown in the bottom part of the
     * listing.
     */
    forfeit: boolean;
    results?: PoleResultFileModel;
}
