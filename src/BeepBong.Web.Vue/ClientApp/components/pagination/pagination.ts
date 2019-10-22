import Vue from 'vue';
import { Model, Component } from 'vue-property-decorator';

import { Pagination } from '../../models/pagination';

@Component
export default class PaginationComponent extends Vue {
    @Model('onload', { type: Object }) pg!: Pagination;

    hasNextPage(): boolean {
        if (this.pg.pageIndex < 0) return false;
        return this.pg.pageIndex < this.pg.totalPages;
    }

    hasPreviousPage(): boolean {
        if (this.pg.pageIndex < 0) return false;
        return this.pg.pageIndex > 1;  
    }

    onFirstPage(): boolean {
        if (this.pg.pageIndex < 0) return true;
        return this.pg.pageIndex == 1; 
    }

    onLastPage(): boolean {
        if (this.pg.pageIndex < 0) return true;
        return this.pg.pageIndex === this.pg.totalPages;
    }
    
    callPage(page: number) {
        this.$emit('changePage', page);
    }

    // // TS
    // hasNextPageTS(): boolean {
    //     return this.pg.hasNextPage;
    // }

    // hasPreviousPageTS(): boolean {
    //     return this.pg.hasPreviousPage; 
    // }

    // onFirstPageTS(): boolean {
    //     return this.pg.onFirstPage; 
    // }

    // onLastPageTS(): boolean {
    //     return this.pg.onLastPage; 
    // }
}