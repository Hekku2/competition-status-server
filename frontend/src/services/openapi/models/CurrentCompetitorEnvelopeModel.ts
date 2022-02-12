/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CurrentCompetitorContentModel } from './CurrentCompetitorContentModel';

/**
 * Envelope for current competitor
 */
export type CurrentCompetitorEnvelopeModel = {
    readonly version?: string | null;
    readonly type?: string | null;
    content?: CurrentCompetitorContentModel;
}
