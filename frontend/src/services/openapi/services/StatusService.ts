/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import { request as __request } from '../core/request';

export class StatusService {

    /**
     * @returns any Success
     * @throws ApiError
     */
    public static async statusGetHealth(): Promise<any> {
        const result = await __request({
            method: 'GET',
            path: `/`,
        });
        return result.body;
    }

}