import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { Library } from '../../../models/library';
import { listResponse } from '../../../models/listResponse';
import { Pagination } from '../../../models/pagination';

@Component
export default class LibraryListView extends Vue {
    libraries: Library[] = [];
    pagination: Pagination = new Pagination;

    mounted()
    {
        feather.replace();
        this.getLibraries(1);
    }

    getLibraries(num: number)
    {
        Axios.get<listResponse<Library>>('api/Library', { params: { pageNumber: num }})
            .then(Response => {
                this.libraries = Response.data.items;
                this.pagination = <Pagination>Response.data;
            })
            .catch(e => console.log(e));
    }

    changePage(num: number)
    {
        this.getLibraries(num);
    }
}