import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ChannelItem } from '../../../models/channel';
import BroadcasterList from '../list/list';
import { BroadcasterItem } from '../../../models/broadcaster';

@Component({
    components: {
        ChannelCard: require('../../../components/ccard/ccard.vue.html').default
    }
})
export default class BroadcastChannels extends Vue {
    broadcaster: BroadcasterItem = new BroadcasterItem();
    channels: ChannelItem[] = [];

    mounted() {
        feather.replace(); //@TODO: Consider removing
        this.getProgrammes();
    }
    
    getProgrammes()
    {
        Axios.get('/api/Broadcaster/' + this.$route.params.id)
            .then(response => {
                this.broadcaster = response.data;
            })
            .catch(e =>
                console.log(e)
            )

        Axios.get('/api/Broadcaster/' + this.$route.params.id + '/Channels')
            .then(Response => {
                this.channels = Response.data.items;
            })
            .catch(e => alert(e));
    }

    updated() {
        feather.replace();
    }
}