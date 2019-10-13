import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./views/home/home.vue.html').default },
    { path: '/counter', component: require('./views/counter/counter.vue.html').default },
    { path: '/fetchdata', component: require('./views/fetchdata/fetchdata.vue.html').default },
    { path: '/programme/:id', component: require('./views/programme/programme.vue.html').default}
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./views/app/app.vue.html').default)
});
