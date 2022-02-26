/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { ScoreboardMode } from './ScoreboardMode';

export type ScoreboardSettingsFileModel = {
    scoreboardMode?: ScoreboardMode;
    /**
     * Name of the currently selected division
     */
    activeDivision?: string | null;
}
