import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { TracklistItem } from '../../../models/tracklist'
import { Pagination } from '../../../models/pagination';
import { listResponse } from '../../../models/listResponse';

@Component({
    components: {
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class OrpanedTracklistView extends Vue {
    tracklists: TracklistItem[] = []
    pagination: Pagination = new Pagination;

    mounted()
    {
        this.getTracklist(1)
    }

    getTracklist(num: number)
    {
		this.tracklists = [];

        Axios.get<listResponse<TracklistItem>>('api/Report/OrphanedTrackList', { params: { pageNumber: num }})
            .then(Response => {
                this.tracklists = Response.data.items;
                this.pagination = <Pagination>Response.data;
            })
    }

    updated()
    {
		feather.replace();
    }

    changePage(page: number)
    {
        this.getTracklist(page);
    }
}