export interface PagedList<T> {

    number: number;
    size: number;
    total: number;
    items: T[];
}
