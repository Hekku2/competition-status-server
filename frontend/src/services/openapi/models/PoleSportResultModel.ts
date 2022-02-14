/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

/**
 * Represents a score in Pole Dance Sport series
 */
export type PoleSportResultModel = {
    /**
     * Calculated total score (A+E+D-HJ).
     * This is used to sort the score board.
     */
    total: number;
    /**
     * Artistic score (A)
     */
    artisticScore: number;
    /**
     * Execution score (E)
     */
    executionScore: number;
    /**
     * Difficulty score (D)
     */
    difficultyScore: number;
    /**
     * Head judge penalty (HJ). This is subtracted from the total.
     */
    headJudgePenalty: number;
}
