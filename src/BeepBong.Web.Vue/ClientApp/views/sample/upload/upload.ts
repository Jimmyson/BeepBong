import Vue from 'vue';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

interface fileData {
	fingerprint: string;
	rate: string;
	count: string;
	bitRate: string;
	bitRateMode: string;
	bitDepth: string;
	codec: string;
	compresion: string;
	notes: string;
}

@Component
export default class SampleUploadView extends Vue {
	file: File | null = null;
	fileData: fileData = {} as fileData;
	inputNotes: string = "";

    mounted() {
        feather.replace();
	}

	handleFileUpload(files: FileList)
	{
		if (files.length > 0)
		{
			this.file = files[0];
			this.fileData.bitDepth = "24";
			this.fileData.codec = "WAVE";
			this.fileData.compresion = "PCM";
			this.fileData.rate = "44100";

			this.processMedia(this.file);
		}

		this.$forceUpdate();
	}

	clearFileUpload()
	{
		this.file = null;
		this.fileData = {} as fileData;
		this.$forceUpdate();
	}

	processMedia(file: File)
	{
		alert('Unable to process Media files due to current tools...');
		// mediainfo(file).then(Response => {
		// 	alert(Response);
		// })
		// .catch(e => {
		// 	alert(e);
		// 	console.log(e);
		// });
	}
}