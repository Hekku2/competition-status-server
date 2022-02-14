/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

import type { PoleResultFileModel } from './PoleResultFileModel';

export type CompetitorResultModel = {
    /**
     * ID of competitor whose results are set
     */
    id: number;
    results?: PoleResultFileModel;
}
