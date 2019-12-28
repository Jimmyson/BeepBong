import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ChannelItem } from '../../../models/channel';
import { ProgrammeItem } from '../../../models/programme';
import { listResponse } from '../../../models/listResponse';
import { Pagination } from '../../../models/pagination';
import moment from 'moment';

@Component({
    components: {
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default,
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class ChannelDetail extends Vue {
    channel: ChannelItem = {} as ChannelItem;
    pagination: Pagination = {} as Pagination;
    programmes: ProgrammeItem[] = [];

    mounted() {
        feather.replace(); //@TODO: Consider removing
        this.getChannel();
        this.getProgrammes(1);
    }

    getChannel()
    {
        Axios.get<ChannelItem>('/api/Channel/' + this.$route.params.id)
            .then(Response => {
				this.channel = Response.data;
				if (this.channel.opened) this.channel.opened = moment(this.channel.opened).format('dddd, D MMMM YYYY');
				if (this.channel.closed) this.channel.closed = moment(this.channel.closed).format('dddd, D MMMM YYYY');
            })
            .catch(e =>
                alert(e)
            );
    }
    
    getProgrammes(num: number)
    {
        Axios.get<listResponse<ProgrammeItem>>('/api/Channel/' + this.$route.params.id + '/Programmes', { params: { pageNumber: num }})
            .then(Response => {
                this.programmes = Response.data.items;
                this.pagination = <Pagination>Response.data;
            })
            .catch(e =>
                alert(e)
            );
    }
	
	deleteChannel()
	{
		if (confirm("Would you like to delete Channel: " + this.channel.name + "?"))
			Axios.delete('/api/Channel/' + this.$route.params.id)
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

    changePage(page: number)
    {
        this.getProgrammes(page);
    }
}