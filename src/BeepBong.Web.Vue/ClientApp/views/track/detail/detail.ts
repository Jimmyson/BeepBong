import Vue from 'vue';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

@Component
export default class TrackView extends Vue {
    mounted() {
        feather.replace(); //@TODO: Consider moving to updated()
    }
}