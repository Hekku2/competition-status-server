/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

/**
 * Represents a single competitor in file. May be a part of team.
 */
export type CompetitorFileModel = {
    /**
     * Name of the competitor
     */
    name: string;
    /**
     * Team, if given.
     */
    team?: string | null;
}
