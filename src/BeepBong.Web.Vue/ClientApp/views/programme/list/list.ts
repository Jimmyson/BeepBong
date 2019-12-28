import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ProgrammeItem } from '../../../models/programme';
import { listResponse } from '../../../models/listResponse';
import { Pagination } from '../../../models/pagination';

@Component({
    components: {
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default,
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class ProgrammeListView extends Vue {
    pagination: Pagination = new Pagination;
    programmes: ProgrammeItem[] = [];

    beforeMount() {
        feather.replace();
        this.getProgrammes(1);
    }
    
    getProgrammes(num: number)
    {
        Axios.get<listResponse<ProgrammeItem>>('/api/Programme', { params: { pageNumber: num }})
            .then(Response => {
                this.programmes = Response.data.items;
                this.pagination = <Pagination>Response.data; // @TODO: Remove list from class
            })
            .catch(e =>
                console.log(e)
            )
    }
    
    updated() {
        feather.replace();
    }

    changePage(page: number)
    {
        this.getProgrammes(page);
    }
}