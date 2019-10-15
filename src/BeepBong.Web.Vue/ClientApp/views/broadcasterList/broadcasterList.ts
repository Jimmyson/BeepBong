import Vue from 'vue';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        BroadcasterCard: require('../../components/bcard/bcard.vue.html').default
    }
})
export default class BroadcasterList extends Vue {
    mounted() { feather.replace(); }
}