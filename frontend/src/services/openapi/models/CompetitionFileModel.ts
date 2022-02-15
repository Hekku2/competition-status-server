/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CurrentCompetitorFileModel } from './CurrentCompetitorFileModel';
import type { DivisionFileModel } from './DivisionFileModel';

/**
 * Describes the filemodel that is used to save current status of
 * competition. This model should hold all information of the competition
 * that can be saved to file.
 */
export type CompetitionFileModel = {
  /**
   * Name of the whole competition. Example: National finals 2022
   */
  name: string;
  /**
   * Divisions for this competition.
   */
  divisions: Array<DivisionFileModel>;
  currentCompetitor?: CurrentCompetitorFileModel;
}
