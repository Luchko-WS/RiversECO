import { Criteria } from './criteria';

export interface Review {
    createdBy: string;
    comment: string;
    waterObjectId: string;
    criterias: Criteria[];
    modifiedBy?: string;
}