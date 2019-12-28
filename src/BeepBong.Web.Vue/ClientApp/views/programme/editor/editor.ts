import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import moment from 'moment';
import { Component } from 'vue-property-decorator';

interface ProgrammeEdit {
	programmeId: string;
	name: string;
	airDate: string;
	channelId: string;
	trackListIds: string[];
	imageId: string;
	imageChange: boolean;
	image: File;
}

// @TODO: Make a TS interface
interface IdList {
	id: string;
	group: string;
	name: string;
}

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default
    }
})
export default class ProgrammeEditorView extends Vue {
	programme: ProgrammeEdit = {} as ProgrammeEdit;

	channelList: IdList[] = [];
	tracklistList: IdList[] = [];

    mounted() {
		feather.replace();
		this.programme.trackListIds = [];
		this.programme.imageChange = false;
		this.getLists();

		if (this.$route.query.id != undefined)
			this.getProgramme();
	}
	
	getProgramme()
	{
		Axios.get<ProgrammeEdit>('api/Programme/' + this.$route.query.id)
			.then(Response => {
				this.programme = Response.data;
				if (this.programme.airDate) this.programme.airDate = moment(this.programme.airDate).format('YYYY-MM-DD');
			})
			.catch(e => alert(e));
	}

	getLists()
	{
		Axios.get<IdList[]>('api/Channel/IdList')
			.then(Response => this.channelList = Response.data)
			.catch(e => alert(e));
		
		Axios.get<IdList[]>('api/TrackList/IdList')
			.then(Response => this.tracklistList = Response.data)
			.catch(e => alert(e));
	}

	sendProgramme()
	{
		var formData = new FormData;
		this.objectToFormData(this.programme, formData);

		if (this.$route.query.id != undefined)
		{
			Axios.put<ProgrammeEdit>('api/Programme/' + this.$route.query.id, formData)
				.then(Response => this.$router.back())
				.catch(e => alert(e))
		} else
		{
			Axios.post<ProgrammeEdit>('api/Programme/', formData)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		}
	}

	handleFileUpload(files: FileList)
	{
		if (files.length > 0)
		{
			this.programme.image = files[0];
			this.programme.imageChange = true
		}

		this.$forceUpdate();
	}

	clearFileUpload()
	{
		this.programme.imageChange = false;
		delete this.programme.image;
		this.$forceUpdate();
	}

	// Move to sepeate TS object
	objectToFormData(obj: any, form: FormData, namespace?: string) {
		var fd = form || new FormData();
		var formKey;
		
		for (var property in obj) {
			if (obj.hasOwnProperty(property)) {
				if(namespace) {
					formKey = namespace + '[' + property + ']';
				} else {
					formKey = property;
				}
				
				// if the property is an object, but not a File,
				// use recursivity.
				if (typeof obj[property] === 'object' && !(obj[property] instanceof File)) {
					this.objectToFormData(obj[property], fd, property);
				} else {
					// if it's a string or a File object
					fd.append(formKey, obj[property]);
				}
			}
		}
		
		return fd;
	};
}