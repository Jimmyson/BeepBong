import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { TracklistItem } from '../../../models/tracklist'

@Component
export default class OrpanedTracklist extends Vue {
    tracklists: TracklistItem[] = []

    mounted()
    {
        this.getTracklist()
    }

    getTracklist()
    {
        Axios.get('api/Report/OrphanedTrackList')
            .then(Response => {
                this.tracklists = Response.data.items;
            })
    }

    updated()
    {
        feather.replace();
    }
}