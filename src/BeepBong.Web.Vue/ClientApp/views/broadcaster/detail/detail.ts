import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ChannelItem } from '../../../models/channel';
import { BroadcasterItem } from '../../../models/broadcaster';
import { listResponse } from '../../../models/listResponse';
import { Pagination } from '../../../models/pagination';

@Component({
    components: {
        ChannelCard: require('../../../components/ccard/ccard.vue.html').default,
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class BroadcastChannelsView extends Vue {
    broadcaster: BroadcasterItem = new BroadcasterItem();
    pagination: Pagination = new Pagination;
    channels: ChannelItem[] = [];

    beforeMount() {
        feather.replace(); //@TODO: Consider removing
        this.getBroadcaster();
        this.getChannels(1);
    }
    
    getBroadcaster()
    {
        Axios.get<BroadcasterItem>('/api/Broadcaster/' + this.$route.params.id)
            .then(response => {
                this.broadcaster = response.data;
            })
            .catch(e =>
                console.log(e)
            )
    }

    getChannels(num: number)
    {
        Axios.get<listResponse<ChannelItem>>('/api/Broadcaster/' + this.$route.params.id + '/Channels', { params: { pageNumber: num }})
            .then(Response => {
                this.channels = Response.data.items;
                this.pagination = <Pagination>Response.data; // @TODO: Remove list from class
            })
            .catch(e => alert(e));
    }

    updated() {
        feather.replace();
    }

    changePage(page: number)
    {
        this.getChannels(page);
    }
}