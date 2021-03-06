import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./views/home/home.vue.html').default },
    
    { path: '/broadcaster', component: require('./views/broadcaster/list/list.vue.html').default }, // List of broadcaster
    { path: '/broadcaster/editor', name: 'broadcaster-editor', component: require('./views/broadcaster/editor/editor.vue.html').default }, // Broadcaster Editor
    { path: '/broadcaster/:id', name: 'broadcaster-detail', component: require('./views/broadcaster/detail/detail.vue.html').default }, // Broadcaster and Channel List
    
    { path: '/channel', component: require('./views/channel/list/list.vue.html').default }, // List of Channels
    { path: '/channel/editor', name: 'channel-editor', component: require('./views/channel/editor/editor.vue.html').default }, // Channel Editor
    { path: '/channel/:id', name: 'channel-detail', component: require('./views/channel/detail/detail.vue.html').default }, // Channel and Programme List

    { path: '/programme', component: require('./views/programme/list/list.vue.html').default }, // List of Programmes
    { path: '/programme/editor', name: 'programme-editor', component: require('./views/programme/editor/editor.vue.html').default }, // Programme Editor
    { path: '/programme/:id', name: 'programme-detail', component: require('./views/programme/detail/detail.vue.html').default }, // Programme and Tracklists

    { path: '/tracklist', component: require('./views/tracklist/list/list.vue.html').default }, // List of Tracklists
    { path: '/tracklist/editor', name: 'tracklist-editor', component: require('./views/tracklist/editor/editor.vue.html').default }, // Tacklist Editor
    { path: '/tracklist/:id', name: 'tracklist-detail', component: require('./views/tracklist/detail/detail.vue.html').default }, // Tracklist and Tracks

    { path: '/library', component: require('./views/library/list/list.vue.html').default },
    { path: '/library/editor', name: 'library-editor', component: require('./views/library/editor/editor.vue.html').default },

    { path: '/track/:id', name: 'track-detail', component: require('./views/track/detail/detail.vue.html').default }, // Track Detail

    // { path: '/sample/upload', name: 'sample-editor', component: require('./views/sample/upload/upload.vue.html').default }, // Sample Upload
    // { path: '/sample/:id', component: require('./views/sample/detail/detail.vue.html').default }, // Sample Detail
    // { path: '/sample/:id/edit', component: require('./views/sample/edit/edit.vue.html').default }, // Sample Edit

    // Reports
    { path: '/report/orphan-tracklist', component: require('./views/reports/orphanedtl/orphanedtl.vue.html').default },
    { path: '/report/programme-wo-tl', component: require('./views/reports/progwotl/progwotl.vue.html').default }
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./views/app/app.vue.html').default)
});
