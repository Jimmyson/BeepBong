import Vue from 'vue';
import feather from 'feather-icons';
import Component from 'vue-class-component';

@Component
export default class TracklistCreatorView extends Vue {
    mounted()
    {
        feather.replace();
    }
}