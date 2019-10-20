import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { TracklistDetail, Track } from '../../../models/tracklistDetail';

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default,
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default
    }
})
export default class TracklistDetailView extends Vue {
    tracklist: TracklistDetail = new TracklistDetail();

    mounted()
    {
        feather.replace(); //@TODO: Consider moving to Updated()
        this.getTracklist();
    }

    getTracklist()
    {
        Axios.get('/api/Tracklist/' + this.$route.params.id)
            .then(response => {
                this.tracklist = response.data;
            })
            .catch(e => console.log(e));
    }
}