/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { ParticipationModel } from '../models/ParticipationModel';
import { request as __request } from '../core/request';

export class CompetitorService {

    /**
     * @returns ParticipationModel Success
     * @throws ApiError
     */
    public static async competitorGetAll(): Promise<Array<ParticipationModel>> {
        const result = await __request({
            method: 'GET',
            path: `/Competitor/all`,
        });
        return result.body;
    }

}