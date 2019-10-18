import Vue from 'vue';
import feather from 'feather-icons';
import { Component, Model } from 'vue-property-decorator';

import { ProgrammeItem } from '../../models/programme';

@Component
export default class ProgrammeCard extends Vue {
    @Model('onload', {type: ProgrammeItem}) readonly p!: ProgrammeItem

    mounted()
    {
        feather.replace();
    }
}