import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { Track } from '../../../models/track';
import { TracklistItem } from '../../../models/tracklist';

// @TODO: Make a TS interface
interface IdList {
	id: string;
	group: string;
	name: string;
}

@Component
export default class TracklistCreatorView extends Vue {
	tracklist: TracklistItem = new TracklistItem;
	
	tracks: Track[] = [];
	libraryList: IdList[] = [];

	bulkWriter: string = "";

    mounted()
    {
		this.getLibraries();
		
		if (this.$route.query.id != undefined)
        	this.getTracklist();
    }

    getTracklist()
    {
        Axios.get('api/Tracklist/' + this.$route.query.id)
            .then(Response => {
				this.tracklist = Response.data;
				this.tracks = this.tracklist.tracks.concat();
            })
			.catch(e => alert(e));
    }

    getLibraries()
    {
        Axios.get<IdList[]>('api/Library/IdList')
            .then(Response => {
                this.libraryList = Response.data;
			})
			.catch(e => alert('Library List: ' + e));
	}

	sendTracklist()
	{
		this.tracklist.tracks = this.tracks;

		if (this.$route.query.id != undefined)
		{
			Axios.put('api/Tracklist/' + this.$route.query.id, this.tracklist)
				.then(Response => {alert('OK'); this.$router.back();})
				.catch(e => alert(e))
		} else
		{
			Axios.post('api/Tracklist/', this.tracklist)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		}
	}
	
	addTrack()
	{
		this.tracks.push({} as Track);
	}

	removeTrack(i: number)
	{
		this.tracks.splice(i, 1);
	}

	updated()
	{
		feather.replace();
	}

    parseBulk()
    {
        this.tracks = [];
        this.bulkWriter.split('\n').forEach(row => {
            var t = new Track();
            var data = row.split('-');

            if (row.trim().length > 0) {
                t.name = data[0] ? data[0].trim() : "";
                t.variant = data[1] ? data[1].trim() : "";
                t.description = data[2] ? data[2].trim() : "";

                if (data.length > 0) this.tracks.push(t);
            }
        })
    }

    parseTracks()
    {
        this.bulkWriter = "";
        this.tracks.forEach(t => {
            this.bulkWriter += (t.name ? t.name : '') + ' - ' + (t.variant ? t.variant : '') + ' - ' + (t.description ? t.description : '') + '\n';
        })
    }
}