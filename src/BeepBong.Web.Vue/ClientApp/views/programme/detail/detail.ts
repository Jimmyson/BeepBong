import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ProgrammeItem } from '../../../models/programme';

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default
    }
})
export default class ProgrammeView extends Vue {
    programme: ProgrammeItem = new ProgrammeItem();

    mounted()
    {
        feather.replace(); //@TODO: Consider removing
        this.getProgramme();
    }
    
    getProgramme()
    {
        Axios.get('/api/Programme/' + this.$route.params.id)
            .then(Response => {
                this.programme = Response.data;
            });
    }

    updated() {
        feather.replace();
    }
}