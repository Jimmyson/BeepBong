import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { TracklistItem } from '../../../models/tracklist';

@Component
export default class TracklistListView extends Vue {
    tracklists: TracklistItem[] = [];

    mounted()
    {
        feather.replace();
        this.getTracklists();
    }

    getTracklists()
    {
        Axios.get('/api/Tracklist')
            .then(response => {
                response.data.items.forEach((element: TracklistItem) => {
                    this.tracklists.push(element);
                });
            }).catch(e => alert(e));
    }

    updated() {
        feather.replace();
    }
}