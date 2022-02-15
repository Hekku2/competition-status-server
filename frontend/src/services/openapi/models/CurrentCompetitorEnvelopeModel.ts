/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CurrentCompetitorContentModel } from './CurrentCompetitorContentModel';

/**
 * Envelope for current competitor. Current competitor is the competitor that
 * is performing currently or is performing next, if no one is performing.
 */
export type CurrentCompetitorEnvelopeModel = {
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
    content?: CurrentCompetitorContentModel;
}
