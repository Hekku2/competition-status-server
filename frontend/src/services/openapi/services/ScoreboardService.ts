/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ScoreboardModeModel } from '../models/ScoreboardModeModel';
import type { ScoreboardStatusModel } from '../models/ScoreboardStatusModel';
import { request as __request } from '../core/request';

export class ScoreboardService {

    /**
     * @returns ScoreboardStatusModel Success
     * @throws ApiError
     */
    public static async scoreboardGetStatus(): Promise<ScoreboardStatusModel> {
        const result = await __request({
            method: 'GET',
            path: `/Scoreboard`,
        });
        return result.body;
    }

    /**
     * @param mode
     * @returns any Success
     * @throws ApiError
     */
    public static async scoreboardSetScoreboardMode(
        mode?: ScoreboardModeModel,
    ): Promise<any> {
        const result = await __request({
            method: 'PUT',
            path: `/Scoreboard/set-mode`,
            query: {
                'mode': mode,
            },
        });
        return result.body;
    }

    /**
     * Sets results that will be shown. Doesn't show the results yet.
     * This is done with "set-mode"
     * @param id id
     * @returns any Success
     * @throws ApiError
     */
    public static async scoreboardSelectResultForShowing(
        id?: number,
    ): Promise<any> {
        const result = await __request({
            method: 'PUT',
            path: `/Scoreboard/select-results`,
            query: {
                'id': id,
            },
        });
        return result.body;
    }

}