/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorPositionFileModel } from './CompetitorPositionFileModel';

/**
 * Describes division that is saved in file. User for JSON conversions.
 */
export type DivisionFileModel = {
    /**
     * Name of the division.
     */
    name: string;
    /**
     * Competitors participating in this division.
     */
    items: Array<CompetitorPositionFileModel>;
}
