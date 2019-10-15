import Vue from 'vue';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        ProgrammeCard: require('../../components/pcard/pcard.vue.html').default
    }
})
export default class ProgrammeList extends Vue {
    mounted() { feather.replace(); }
}