import { Criteria } from './criteria';
import { WaterObject } from './water-object';

export interface Review {
    createdBy: string;
    comment: string;
    waterObjectId: string;
    waterObject?: WaterObject;
    criterias: Criteria[];
    modifiedBy?: string;
}
