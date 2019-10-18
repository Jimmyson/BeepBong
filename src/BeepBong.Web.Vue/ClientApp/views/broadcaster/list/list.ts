import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { BroadcasterItem } from '../../../models/broadcaster';

@Component({
    components: {
        BroadcasterCard: require('../../../components/bcard/bcard.vue.html').default
    }
})
export default class BroadcasterList extends Vue {
    broadcasters: BroadcasterItem[] = [];

    mounted() {
        feather.replace();
        this.getProgrammes();
    }
    
    getProgrammes()
    {
        Axios.get('/api/Broadcaster')
            .then(response => {
                response.data.items.forEach((element: { name: string; year: string}) => {
                    var item = new BroadcasterItem();
                    item.name = element.name;
                    item.airDate = element.year;

                    this.broadcasters.push(item);
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