/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { DivisionListModel } from '../models/DivisionListModel';
import { request as __request } from '../core/request';

export class DivisionService {

    /**
     * @returns DivisionListModel Success
     * @throws ApiError
     */
    public static async divisionGetAll(): Promise<Array<DivisionListModel>> {
        const result = await __request({
            method: 'GET',
            path: `/Division/all`,
        });
        return result.body;
    }

}