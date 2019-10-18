export class TracklistItem {
    constructor() {
        this.trackListId = "";
        this.name = "";
        this.composer = "";
        this.library = false;
        this.trackCount = 0;
        this.programmeCount = 0;
    }

    trackListId: string;
    name: string;
    composer: string;
    library: boolean;
    trackCount: number;
    programmeCount: number;
}