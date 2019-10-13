import feather from 'feather-icons';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        MenuComponent: require('../../components/navmenu/navmenu.vue.html').default
    }
})
export default class AppComponent extends Vue {
    mounted()
    {
        feather.replace();
    }
}
