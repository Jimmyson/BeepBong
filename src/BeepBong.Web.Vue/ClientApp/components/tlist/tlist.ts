import Vue from 'vue';
import feather from 'feather-icons';
import { Component, Model } from 'vue-property-decorator';

import { TracklistDetail } from '../../models/tracklistDetail';

@Component
export default class TracklistDetailItem extends Vue {
    @Model('onload', {type: TracklistDetail}) readonly tl!: TracklistDetail

    updated()
    {
        feather.replace();
    }
}