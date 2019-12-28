import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { TracklistDetail, Track } from '../../../models/tracklistDetail';
import { ProgrammeItem } from '../../../models/programme';
import { listResponse } from '../../../models/listResponse';
import { Pagination } from '../../../models/pagination';

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default,
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default
    }
})
export default class TracklistDetailView extends Vue {
	tracklist: TracklistDetail = new TracklistDetail();
	programmes: ProgrammeItem[] = [];
	pagination: Pagination = {} as Pagination;

    created()
    {
		this.getTracklist();
		this.getProgrammes();
    }

    getTracklist()
    {
        Axios.get('/api/Tracklist/' + this.$route.params.id)
            .then(response => {
                this.tracklist = response.data;
            })
            .catch(e => console.log(e));
	}
	
	getProgrammes()
	{
		Axios.get<listResponse<ProgrammeItem>>('/api/Tracklist/' + this.$route.params.id + '/Programmes')
		.then(Response => {
			this.programmes = Response.data.items;
			this.pagination = <Pagination>Response.data;
		})
	}
	
	deleteTracklist()
	{
		if (confirm("Would you like to delete Tracklist: " + this.tracklist.name + "?"))
			Axios.delete('/api/Tracklist/' + this.$route.params.id)
				.then(response => {
					this.$router.back();
				})
				.catch(e =>
					console.log(e)
				);
	}

	updated()
	{
		feather.replace(); //@TODO: Consider moving to Updated()
	}
}