import Vue from 'vue';
import feather from 'feather-icons';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        ChannelCard: require('../../components/ccard/ccard.vue.html').default
    }
})
export default class ChannelList extends Vue {
    mounted() { feather.replace(); }
}