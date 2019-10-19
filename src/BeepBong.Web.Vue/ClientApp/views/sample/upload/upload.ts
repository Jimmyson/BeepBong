import Vue from 'vue';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

@Component
export default class SampleUploadView extends Vue {
    mounted() {
        feather.replace();
    }
}