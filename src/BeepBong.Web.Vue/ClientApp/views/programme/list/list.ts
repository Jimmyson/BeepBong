import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ProgrammeItem } from '../../../models/programme';

@Component({
    components: {
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default
    }
})
export default class ProgrammeListView extends Vue {
    programmes: ProgrammeItem[] = [];

    mounted() {
        feather.replace();
        this.getProgrammes();
    }
    
    getProgrammes()
    {
        Axios.get('/api/Programme')
            .then(response => {
                response.data.items.forEach((element: { name: string; year: string; channel: string; }) => {
                    var item = new ProgrammeItem();
                    item.name = element.name;
                    item.airDate = element.year;
                    item.channelName = element.channel;

                    this.programmes.push(item);
                });
                // this.programmes[0].name = response.data.items[0].name;
                // this.programmes[0].airDate = response.data.items[0].year;
                // this.programmes[0].channelName = response.data.items[0].channel;
            })
            .catch(e =>
                console.log(e)
            )
    }
    
    updated() {
        feather.replace();
    }
}