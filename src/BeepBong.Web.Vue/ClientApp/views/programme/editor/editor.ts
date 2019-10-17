import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default
    }
})
export default class ProgrammeEditorView extends Vue {
    mounted()
    {
        feather.replace();
        //this.getProgramme();
    }
}