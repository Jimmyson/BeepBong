import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

import { ProgrammeItem } from '../../../models/programme'
import { Pagination } from '../../../models/pagination';
import { listResponse } from '../../../models/listResponse';
import moment from 'moment';

@Component({
    components: {
        PaginationItem: require('../../../components/pagination/pagination.vue.html').default
    }
})
export default class OrpanedTracklist extends Vue {
    programmes: ProgrammeItem[] = []
    pagination: Pagination = new Pagination;

    mounted()
    {
        this.getProgrammes(1)
    }

    getProgrammes(num: number)
    {
        Axios.get<listResponse<ProgrammeItem>>('api/Report/ProgrammeWOTrackList', { params: { pageNumber: num }})
            .then(Response => {
                this.programmes = Response.data.items;
                this.pagination = <Pagination>Response.data;
                this.programmes.forEach((item) => {
                    if (item.airDate) item.airDate = moment(item.airDate).format('dddd, D MMMM YYYY');
                })
            })
    }

    updated()
    {
        feather.replace();
    }

    changePage(page: number)
    {
        this.getProgrammes(page);
    }
}