import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

class Programme {
    imageId: string;
    name: string;
    airDate: string;
    channel: string;

    constructor()
    {
        this.imageId = "";
        this.name = "";
        this.airDate = "";
        this.channel = "";
    }
}

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default
    }
})
export default class ProgrammeView extends Vue {
    programme: Programme = new Programme();

    mounted()
    {
        feather.replace(); //@TODO: Consider removing
        this.getProgramme();
    }
    
    getProgramme()
    {
        Axios.get('/api/Programme/' + this.$route.params.id)
            .then(Response => {
                this.programme.imageId = Response.data.imageId;
                this.programme.name = Response.data.name;
                this.programme.airDate = Response.data.airDate;
                this.programme.channel = Response.data.channelName;
            });
    }

    updated() {
        feather.replace();
    }
}