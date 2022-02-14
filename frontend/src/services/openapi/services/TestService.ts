/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CurrentCompetitorsEntity } from '../models/CurrentCompetitorsEntity';
import { request as __request } from '../core/request';

export class TestService {

    /**
     * @param requestBody
     * @returns any Success
     * @throws ApiError
     */
    public static async testLoadPoleCompetitionData(
        requestBody?: CurrentCompetitorsEntity,
    ): Promise<any> {
        const result = await __request({
            method: 'POST',
            path: `/Test/set-current-competitors`,
            body: requestBody,
        });
        return result.body;
    }

}