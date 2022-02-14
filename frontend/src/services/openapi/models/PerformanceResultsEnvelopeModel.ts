/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { PerformanceResultsContentModel } from './PerformanceResultsContentModel';

export type PerformanceResultsEnvelopeModel = {
    readonly version?: string | null;
    readonly type?: string | null;
    content?: PerformanceResultsContentModel;
}
