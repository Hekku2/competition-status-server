/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

/**
 * Model used to set ID of current competitor(s)
 */
export type CurrentCompetitorSetModel = {
    /**
     * ID of currently active competitor (or who is performing next if no
     * one is active).
     *
     * If null is used, active competitor is cleared.
     */
    id?: number | null;
}
