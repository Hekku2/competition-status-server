/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { CompetitorEntity } from './CompetitorEntity';

export type CurrentCompetitorsEntity = {
    id?: number | null;
    division?: string | null;
    competitors?: Array<CompetitorEntity> | null;
}
