import { Criteria } from './criteria';

export interface Review {
    author: string;
    comment: string;
    criterias: Criteria[];
}