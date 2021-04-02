import { Criteria } from './criteria';
import { WaterObject } from './water-object';

export interface Review {
    waterObjectId: string;
    waterObject?: WaterObject;
    createdBy: string;
    isAnonymous: boolean;
    criteria: Criteria;
    influence: number;
    referenceType: number;
    reference: string;
    comment: string;
    modifiedBy?: string;
    status: number;
    certainty?: number;
}

export interface ReviewCreateModel {
    waterObjectId: string;
    createdBy: string;
    isAnonymous: boolean;
    criteriaName: string;
    influence: number;
    referenceType: number;
    reference: string;
    comment: string;
}