import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { Track } from '../../../models/track';

@Component
export default class TrackView extends Vue {
    track: Track = new Track();
    samples: any[] = [];

    mounted() {
        feather.replace(); //@TODO: Consider moving to updated()
        this.getTrack();
    }

    getTrack()
    {
        Axios.get('api/Track/' + this.$route.params.id)
            .then(Response => {
                this.track = Response.data;
            })
            .catch(e => console.log(e));

            
        Axios.get('api/Track/' + this.$route.params.id + '/Samples')
            .then(Response => {
                this.samples = Response.data.items;
            })
            .catch(e => console.log(e));
    }
}