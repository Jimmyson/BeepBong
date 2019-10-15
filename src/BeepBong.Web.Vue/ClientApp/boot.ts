import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./views/home/home.vue.html').default },
    { path: '/counter', component: require('./views/counter/counter.vue.html').default }, // To be removed
    { path: '/fetchdata', component: require('./views/fetchdata/fetchdata.vue.html').default }, // To be removed
    { path: '/broadcaster', component: require('./views/broadcasterlist/broadcasterlist.vue.html').default },
    { path: '/channel', component: require('./views/channellist/channellist.vue.html').default },
    { path: '/programme', component: require('./views/programmelist/programmelist.vue.html').default },
    { path: '/programme/:id', component: require('./views/programme/programme.vue.html').default },
    { path: '/track/:id', component: require('./views/track/track.vue.html').default },
    { path: '/tracklist/new', component: require('./views/tracklistCreator/tracklistCreator.vue.html').default },
    { path: '/sample/upload', component: require('./views/sampleUpload/sampleUpload.vue.html').default }
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./views/app/app.vue.html').default)
});
