export interface Criteria {
    name: string;
    description: string;
}

export interface CheckedCriteria extends Criteria {
    checked: boolean;
}