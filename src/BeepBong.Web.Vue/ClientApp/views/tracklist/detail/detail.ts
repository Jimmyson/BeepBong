import Vue from 'vue';
import feather from 'feather-icons';
import Component from 'vue-class-component';

@Component({
    components: {
        Tracklist: require('../../../components/tlist/tlist.vue.html').default,
        ProgrammeCard: require('../../../components/pcard/pcard.vue.html').default
    }
})
export default class TracklistDetailView extends Vue {
    mounted()
    {
        feather.replace();
    }
}