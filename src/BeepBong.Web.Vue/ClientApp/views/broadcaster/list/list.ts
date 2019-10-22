import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { BroadcasterItem } from '../../../models/broadcaster';
import { Pagination } from '../../../models/pagination';
import { listResponse } from '../../../models/listResponse';

@Component({
    components: {
        BroadcasterCard: require('../../../components/bcard/bcard.vue.html').default,
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class BroadcasterListView extends Vue {
    broadcasters: BroadcasterItem[] = [];
    pagination: Pagination = new Pagination;

    mounted() {
        feather.replace();
        this.getBroadcasters(1);
    }
    
    getBroadcasters(num: number)
    {
        Axios.get<listResponse<BroadcasterItem>>('/api/Broadcaster', { params: { pageNumber: num }})
            .then(Response => {
                this.broadcasters = Response.data.items;
                this.pagination = <Pagination>Response.data;// @TODO: Remove list from class
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
        this.getBroadcasters(page);
    }
}