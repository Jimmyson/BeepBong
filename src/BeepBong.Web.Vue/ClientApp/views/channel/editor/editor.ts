import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';
import { ChannelItem } from '../../../models/channel';
import moment from 'moment';

// @TODO: Make a TS interface
interface IdList {
	id: string;
	name: string;
}

@Component
export default class ChannelEditor extends Vue {
	channel: ChannelItem = {} as ChannelItem;
	broadcasterList: IdList = {} as IdList;
	
    beforeMount() {
		feather.replace();
		this.getBroadcasterList();
		if (this.$route.query.id != undefined)
			this.getChannel();
    }

    getChannel()
    {
        Axios.get<ChannelItem>('/api/Channel/' + this.$route.query.id)
            .then(Response => {
				this.channel = Response.data;
				if (this.channel.opened) this.channel.opened = moment(this.channel.opened).format('YYYY-MM-DD');
				if (this.channel.closed) this.channel.closed = moment(this.channel.closed).format('YYYY-MM-DD');
            })
            .catch(e =>
                console.log(e)
            );
    }

	getBroadcasterList()
	{
		Axios.get<IdList>('api/Broadcaster/IdList')
			.then(Response => this.broadcasterList = Response.data)
			.catch(e => alert(e));
	}

    sendChannel()
    {
		if (this.$route.query.id != undefined)
		{
			Axios.put<ChannelItem>('api/Channel/' + this.$route.query.id, this.channel)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		} else
		{
			Axios.post<ChannelItem>('api/Channel/', this.channel)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		}
	}
}