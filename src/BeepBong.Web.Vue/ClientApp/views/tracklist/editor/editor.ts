import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { Track } from '../../../models/track';
import { TracklistItem } from '../../../models/tracklist';

@Component
export default class TracklistCreatorView extends Vue {
    tracklist: TracklistItem = new TracklistItem;
    tracks: Track[] = []
    bulkWriter: string = "";

    mounted()
    {
        feather.replace();
        this.getTracklist();
    }

    getTracklist()
    {
        Axios.get('api/Tracklist/' + this.$route.query.id)
            .then(Response => {
                this.tracklist = Response.data;
            })
    }

    parseBulk()
    {
        this.tracks = [];
        this.bulkWriter.split('\n').forEach(row => {
            var t = new Track();
            var data = row.split('-');

            if (row.trim().length > 0) {
                if (data[0]) t.name = data[0].trim();
                if (data[1]) t.variant = data[1].trim();
                if (data[2]) t.description = data[2].trim();

                if (data.length > 0) this.tracks.push(t);
            }
        })
    }

    parseTracks()
    {
        this.bulkWriter = "";
        this.tracks.forEach(t => {
            this.bulkWriter += t.name + " - " + t.variant + " - " + t.description;
        })
    }
}