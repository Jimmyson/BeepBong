import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

interface BroadcasterEdit {
	broadcasterId: string;
	name: string;
	country: string;
	imageId: string;
	imageChange: boolean;
	image: File;
}

@Component
export default class BroadcastEditor extends Vue {
	broadcaster: BroadcasterEdit = {} as BroadcasterEdit;

    mounted() {
		feather.replace();
		this.broadcaster.imageChange = false;
		if (this.$route.query.id != undefined)
			this.getBroadcaster();
	}
	
	getBroadcaster()
	{
		Axios.get<BroadcasterEdit>('api/Broadcaster/' + this.$route.query.id)
			.then(Response => this.broadcaster = Response.data)
			.catch(e => alert(e));
	}

	sendBroadcaster()
	{
		var formData = new FormData;
		this.objectToFormData(this.broadcaster, formData);

		if (this.$route.query.id != undefined)
		{
			Axios.put<BroadcasterEdit>('api/Broadcaster/' + this.$route.query.id, formData)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		} else
		{
			Axios.post<BroadcasterEdit>('api/Broadcaster/', formData)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		}
	}

	handleFileUpload(files: FileList)
	{
		if (files.length > 0)
		{
			this.broadcaster.image = files[0];
			this.broadcaster.imageChange = true
		}

		this.$forceUpdate();
	}

	clearFileUpload()
	{
		this.broadcaster.imageChange = false;
		delete this.broadcaster.image;
		this.$forceUpdate();
	}

	// Move to seperate TS object
	objectToFormData(obj: any, form: FormData, namespace?: string) {
		var fd = form || new FormData();
		var formKey;
		
		for(var property in obj) {
			if(obj.hasOwnProperty(property)) {
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