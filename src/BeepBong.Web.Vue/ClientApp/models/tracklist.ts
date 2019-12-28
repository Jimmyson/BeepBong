import { Track } from "./track";

export class TracklistItem {
    constructor() {
        this.trackListId = "";
        this.name = "";
        this.composer = "";
        this.library = false;
        this.trackCount = 0;
		this.programmeCount = 0;
		this.tracks = [];
    }

    trackListId: string;
    name: string;
    composer: string;
    library: boolean;
    trackCount: number;
	programmeCount: number;
	tracks: Track[];
}