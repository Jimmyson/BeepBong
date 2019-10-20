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
                response.data.items.forEach((element: ProgrammeItem ) => {
                    this.programmes.push(element);
                });
            })
            .catch(e =>
                console.log(e)
            )
    }
    
    updated() {
        feather.replace();
    }
}