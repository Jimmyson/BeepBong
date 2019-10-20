import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ChannelItem } from '../../../models/channel';
import { ProgrammeItem } from '../../../models/programme';

@Component({
    components: {
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default
    }
})
export default class ChannelDetail extends Vue {
    channel: ChannelItem = new ChannelItem();
    programmes: ProgrammeItem[] = [];

    mounted() {
        feather.replace(); //@TODO: Consider removing
        this.getProgrammes();
    }
    
    getProgrammes()
    {
        Axios.get('/api/Channel/' + this.$route.params.id)
            .then(response => {
                this.channel = response.data;
            })
            .catch(e =>
                console.log(e)
            );
        
        Axios.get('/api/Channel/' + this.$route.params.id + '/Programmes')
            .then(response => {
                console.log(response.data);
                this.programmes = response.data.items;
            })
            .catch(e =>
                alert(e)
            );
    }
    
    updated() {
        feather.replace();
    }
}