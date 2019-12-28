export class TracklistDetail {
    constructor() {
        this.trackListId = "";
        this.name = "";
        this.composer = "";
        this.library = false;
        this.tracks = [];
    }

    trackListId: string;
    name: string;
    composer: string;
    library: boolean;
    tracks: Track[]
}

export class Track {
    constructor() {
        this.trackId = "123";
        this.name = "Track";
        this.description = "You";
        this.note = "Why";
        this.fingerprints = ["shted", "s8ehrd", "sjv73n"];
        this.duration = "0:00:00";
    }

    trackId: string;
    name: string;
    description: string;
    note: string;
    fingerprints: string[];
    duration: string;
}