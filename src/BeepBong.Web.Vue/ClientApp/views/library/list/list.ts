import Vue from 'vue';
import Axios from 'axios';
import feather from 'feather-icons';
import Component from 'vue-class-component';

import { Library } from '../../../models/library';

@Component
export default class LibraryListView extends Vue {
    libraries: Library[] = [];

    mounted()
    {
        feather.replace();
        this.getLibraries();
    }

    getLibraries()
    {
        Axios.get('api/Library')
            .then(Response => {
                this.libraries = Response.data.items;
            })
            .catch(e => console.log(e));
    }
}