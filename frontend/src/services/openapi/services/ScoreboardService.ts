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

}