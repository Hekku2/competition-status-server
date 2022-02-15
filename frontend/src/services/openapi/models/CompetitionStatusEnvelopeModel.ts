/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitionStatusContentModel } from './CompetitionStatusContentModel';

/**
 * Contains current status of competition, of competition is active.
 * If competition is not active, Content can be null.
 */
export type CompetitionStatusEnvelopeModel = {
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
    content?: CompetitionStatusContentModel;
}
