import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ChannelItem } from '../../../models/channel';
import { Pagination } from '../../../models/pagination';
import { listResponse } from '../../../models/listResponse';

@Component({
    components: {
        ChannelCard: require('../../../components/ccard/ccard.vue.html').default,
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class ChannelListView extends Vue {
    pagination: Pagination = new Pagination;
    channels: ChannelItem[] = [];

    beforeMount() {
        feather.replace();
        this.getChannels(1);
    }
    
    getChannels(num: number)
    {
        Axios.get<listResponse<ChannelItem>>('/api/Channel', { params: { pageNumber: num }})
            .then(Response => {
                this.channels = Response.data.items;
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
        this.getChannels(page);
    }
}