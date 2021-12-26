import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import * as axios from 'axios'
import VueToastr from "vue-toastr";
import { BootstrapVue, IconsPlugin, ModalPlugin, NavbarPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './services/chatService.js'

Vue.use(require('vue-moment'));

axios.interceptors.request.use(function (config) {
    var userCredentials = localStorage.getItem('user');
    var key;
    if (userCredentials == null) key = 'some-falsy-key';
    else key = JSON.parse(userCredentials).token;
    config.headers.Authorization = key;
    return config;
})
Vue.prototype.$axios = axios
Vue.use(BootstrapVue).use(IconsPlugin).use(NavbarPlugin).use(ModalPlugin).use(VueToastr)
Vue.config.productionTip = false

new Vue({
    router,
    store,
    axios,
    render: h => h(App),
    mounted() {
        this.$toastr.defaultPosition = "toast-top-right";
    }
}).$mount('#app')