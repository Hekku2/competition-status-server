/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { PerformanceResultsContentModel } from './PerformanceResultsContentModel';

/**
 * Message containing performance results
 */
export type PerformanceResultsEnvelopeModel = {
    /**
     * Envelope version number. Version can be discarded if no
     * functionality is specified for given version
     */
    readonly version?: string | null;
    /**
     * Type of the message. This and version can be used to identify
     * correct parser for this message.
     */
    readonly type?: string | null;
    content?: PerformanceResultsContentModel;
}
