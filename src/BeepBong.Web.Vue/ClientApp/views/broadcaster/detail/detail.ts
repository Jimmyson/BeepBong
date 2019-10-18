import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ChannelItem } from '../../../models/channel';

@Component({
    components: {
        ChannelCard: require('../../../components/ccard/ccard.vue.html').default
    }
})
export default class BroadcastChannels extends Vue {
    channels: ChannelItem[] = [];

    mounted() {
        feather.replace();
        this.getProgrammes();
    }
    
    getProgrammes()
    {
        Axios.get('/api/Broadcaster/aceba20a-dde0-4148-998f-df524996c494')
            .then(response => {
                response.data.channelNames.forEach((element: string) => {
                    var item = new ChannelItem();
                    item.name = element;
                    //item.airDate = element.year;

                    this.channels.push(item);
                });
                // this.programmes[0].name = response.data.items[0].name;
                // this.programmes[0].airDate = response.data.items[0].year;
                // this.programmes[0].channelName = response.data.items[0].channel;
            })
            .catch(e =>
                console.log(e)
            )
    }
}