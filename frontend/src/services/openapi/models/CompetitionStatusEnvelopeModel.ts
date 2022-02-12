/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitionStatusContentModel } from './CompetitionStatusContentModel';

export type CompetitionStatusEnvelopeModel = {
    readonly version?: string | null;
    readonly type?: string | null;
    content?: CompetitionStatusContentModel;
}
