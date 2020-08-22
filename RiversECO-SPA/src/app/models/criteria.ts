export interface Criteria {
    id: string;
    name: string;
    description?: string;
}

export interface CheckedCriteria extends Criteria {
    checked: boolean;
}
