import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { TracklistItem } from '../../../models/tracklist';
import { listResponse } from '../../../models/listResponse';
import { Pagination } from '../../../models/pagination';

@Component({
    components: {
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class TracklistListView extends Vue {
    tracklists: TracklistItem[] = [];
    pagination: Pagination = new Pagination;

    mounted()
    {
        feather.replace();
        this.getTracklists(1);
    }

    getTracklists(num: number)
    {
        Axios.get<listResponse<TracklistItem>>('/api/Tracklist', { params: { pageNumber: num }})
            .then(Response => {
                this.tracklists = Response.data.items;
                this.pagination = <Pagination>Response.data;
            }).catch(e => alert(e));
    }

    updated() {
        //feather.replace(); // Not Redrawing when pages change
    }

    changePage(page: number)
    {
        this.getTracklists(page);
    }
}