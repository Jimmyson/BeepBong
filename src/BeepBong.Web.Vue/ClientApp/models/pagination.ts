export class Pagination {
    constructor()
    {
        this.pageIndex = 1;
        this.totalItems = 0;
        this.totalPages = 1;
    }

    pageIndex: number;
    totalItems: number;
    totalPages: number;

    // get hasNextPage(): boolean {
    //     if (this.pageIndex < 0) return false;
    //     return this.pageIndex < this.totalPages;
    // }

    // get hasPreviousPage(): boolean {
    //     if (this.pageIndex < 0) return false;
    //     return this.pageIndex > 1; 
    // }

    // get onFirstPage(): boolean {
    //     if (this.pageIndex < 0) return true;
    //     return this.pageIndex == 1; 
    // }

    // get onLastPage(): boolean {
    //     if (this.pageIndex < 0) return true;
    //     return this.pageIndex == this.totalPages; 
    // }
}