import Vue from 'vue';
import { Component, Model } from 'vue-property-decorator';

import { ProgrammeItem } from '../../models/programme';

@Component
export default class ProgrammeCard extends Vue {
    @Model('onload', {type: Object}) readonly p!: ProgrammeItem
}