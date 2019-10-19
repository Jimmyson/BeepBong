import Vue from 'vue';
import feather from 'feather-icons';
import { Component,  Model } from 'vue-property-decorator';

import { ChannelItem } from '../../models/channel';

@Component
export default class ChannelCard extends Vue {
    @Model('onload', {type: ChannelItem}) readonly c!: ChannelItem
}