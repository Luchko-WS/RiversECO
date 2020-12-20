import { Criteria } from './criteria';
import { WaterObject } from './water-object';

export interface Review {
    createdBy: string;
    comment: string;
    waterObjectId: string;
    waterObject?: WaterObject;
    criteria: Criteria;
    modifiedBy?: string;
}

export interface ReviewCreateModel {
    createdBy: string;
    comment: string;
    waterObjectId: string;
    criteriaName: string;
    modifiedBy?: string;
    influence?: number;
    globalInfluence?: number;
    references: string;
}