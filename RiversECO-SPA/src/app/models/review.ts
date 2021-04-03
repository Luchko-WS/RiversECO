import { Criteria } from './criteria';
import { WaterObject } from './water-object';

export interface Review {
    waterObject: WaterObject;
    createdBy: string;
    isAnonymous: boolean;
    criteria: Criteria;
    influence: string;
    referenceType: string;
    reference: string;
    comment: string;
    modifiedBy?: string;
    status: string;
    certainty?: number;
}

export interface ReviewCreateModel {
    waterObjectId: string;
    createdBy: string;
    isAnonymous: boolean;
    criteriaName: string;
    influence: string;
    referenceType: string;
    reference: string;
    comment: string;
}
