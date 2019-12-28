import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import moment from 'moment';
import { Component } from 'vue-property-decorator';

import { ProgrammeItem } from '../../../models/programme';
import { TracklistDetail } from '../../../models/tracklistDetail';

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default
    }
})
export default class ProgrammeView extends Vue {
	programme: ProgrammeItem = {} as ProgrammeItem;
	trackListIds: [] = [];
	tracklists: TracklistDetail[] = [];

    beforeMount()
    {
		this.getProgramme();
    }
    
    getProgramme()
    {
        Axios.get('/api/Programme/' + this.$route.params.id)
            .then(Response => {
				this.programme = Response.data;
				this.trackListIds = Response.data.trackListIds;
				
				this.trackListIds.forEach((id) => {
					Axios.get('/api/Tracklist/' + id)
						.then(Response => {
							this.tracklists.push(Response.data);
						})
				});
				this.programme.airDate = moment(this.programme.airDate).format('dddd, D MMMM YYYY');
			});
	}
	
	deleteProgramme()
	{
		if (confirm("Would you like to delete Programme: " + this.programme.name + "?"))
			Axios.delete('/api/Programme/' + this.$route.params.id)
				.then(response => {
					this.$router.back();
				})
				.catch(e =>
					console.log(e)
				);
	}

    updated() {
        feather.replace();
    }
}