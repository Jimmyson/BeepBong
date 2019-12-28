import { Pagination } from './pagination'

export class listResponse<T> extends Pagination {
    items: Array<T> = [];
}