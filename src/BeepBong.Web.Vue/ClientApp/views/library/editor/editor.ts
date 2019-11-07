import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { Library } from '../../../models/library';

@Component
export default class LibraryEditorView extends Vue {
	library: Library = {} as Library;

    mounted() {
		feather.replace();

		if (this.$route.query.id != undefined)
			this.getLibrary();
	}
	
	getLibrary()
	{
		Axios.get<Library>('api/Library/' + this.$route.query.id)
			.then(Response => {
				this.library = Response.data;
			})
			.catch(e => alert(e));
	}

	sendLibrary()
	{
		if (this.$route.query.id != undefined)
		{
			Axios.put<Library>('api/Library/' + this.$route.query.id, this.library)
				.then(Response => this.$router.back())
				.catch(e => alert(e))
		} else
		{
			Axios.post<Library>('api/Library/', this.library)
				.then(Response => alert("OK"))
				.catch(e => alert(e))
		}
	}
}