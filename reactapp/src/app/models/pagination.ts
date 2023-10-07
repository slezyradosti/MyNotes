export interface Pagination {
    PageIndex: number; //currentPAge
    PageSize: number; // itemsPerPage
    RecordCount: number; // totalItems
    PageCount: number; //totalPAges
}

export class PaginatedResult<T> {
    data: T;
    pagination: Pagination;

    constructor(data: T, pagination: Pagination) {
        this.data = data;
        this.pagination = pagination;
    }
}

export class PagingParams {
    paramPageIndex;
    paramPageSize;

    constructor(pageIndex = 0, pageSize = 15) {
        this.paramPageIndex = pageIndex;
        this.paramPageSize = pageSize;
    }
}