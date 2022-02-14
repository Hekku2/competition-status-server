/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { CompetitionFileModel } from '../models/CompetitionFileModel';
import type { CompetitionStatusEnvelopeModel } from '../models/CompetitionStatusEnvelopeModel';
import type { CompetitorResultModel } from '../models/CompetitorResultModel';
import type { CurrentCompetitorEnvelopeModel } from '../models/CurrentCompetitorEnvelopeModel';
import type { CurrentCompetitorSetModel } from '../models/CurrentCompetitorSetModel';
import type { PerformanceResultsEnvelopeModel } from '../models/PerformanceResultsEnvelopeModel';
import { request as __request } from '../core/request';

export class CompetitionService {

    /**
     * Current competitor information
     * @returns CurrentCompetitorEnvelopeModel Success
     * @throws ApiError
     */
    public static async competitionGetCurrentCompetitor(): Promise<CurrentCompetitorEnvelopeModel> {
        const result = await __request({
            method: 'GET',
            path: `/Competition/current-competitor`,
        });
        return result.body;
    }

    /**
     * @returns PerformanceResultsEnvelopeModel Success
     * @throws ApiError
     */
    public static async competitionGetResults(): Promise<PerformanceResultsEnvelopeModel> {
        const result = await __request({
            method: 'GET',
            path: `/Competition/results`,
        });
        return result.body;
    }

    /**
     * Sets current competitor or remove current competitor.
     * @param requestBody Competitor ID
     * @returns any Success
     * @throws ApiError
     */
    public static async competitionSetCurrentCompetitor(
        requestBody?: CurrentCompetitorSetModel,
    ): Promise<any> {
        const result = await __request({
            method: 'POST',
            path: `/Competition/set-current-competitor`,
            body: requestBody,
        });
        return result.body;
    }

    /**
     * Set result for competitor
     * @param requestBody
     * @returns any Success
     * @throws ApiError
     */
    public static async competitionSetResult(
        requestBody?: CompetitorResultModel,
    ): Promise<any> {
        const result = await __request({
            method: 'POST',
            path: `/Competition/set-result`,
            body: requestBody,
        });
        return result.body;
    }

    /**
     * Upload compettion data. This overrides all data
     * @param requestBody model representing the json file
     * @returns any Success
     * @throws ApiError
     */
    public static async competitionUploadCompetition(
        requestBody?: CompetitionFileModel,
    ): Promise<any> {
        const result = await __request({
            method: 'POST',
            path: `/Competition/upload-competition`,
            body: requestBody,
        });
        return result.body;
    }

    /**
     * Returns current status for the whole competition
     * @returns CompetitionStatusEnvelopeModel Success
     * @throws ApiError
     */
    public static async competitionGetCompetitionStatus(): Promise<CompetitionStatusEnvelopeModel> {
        const result = await __request({
            method: 'GET',
            path: `/Competition/competition-status`,
        });
        return result.body;
    }

    /**
     * Returns competition status in file model.
     * @returns CompetitionFileModel Success
     * @throws ApiError
     */
    public static async competitionDownloadCompetition(): Promise<CompetitionFileModel> {
        const result = await __request({
            method: 'GET',
            path: `/Competition/download-competition`,
        });
        return result.body;
    }

}