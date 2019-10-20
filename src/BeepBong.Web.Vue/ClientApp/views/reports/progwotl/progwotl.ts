import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ProgrammeItem } from '../../../models/programme'

@Component
export default class OrpanedTracklist extends Vue {
    programmes: ProgrammeItem[] = []

    mounted()
    {
        this.getProgrammes()
    }

    getProgrammes()
    {
        Axios.get('api/Report/ProgrammeWOTrackList')
            .then(Response => {
                this.programmes = Response.data.items;
            })
    }

    updated()
    {
        feather.replace();
    }
}