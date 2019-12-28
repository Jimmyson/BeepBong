import Vue from 'vue';
import feather from 'feather-icons';
import { Component, Model } from 'vue-property-decorator';

import { BroadcasterItem } from '../../models/broadcaster';

@Component
export default class ProgrammeCard extends Vue {
    @Model('onload', {type: Object}) readonly b!: BroadcasterItem
}