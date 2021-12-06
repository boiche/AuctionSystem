import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import * as axios from 'axios'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './services/chatService.js'

import {
    NavbarPlugin
} from 'bootstrap-vue'
Vue.use(NavbarPlugin)

Vue.prototype.$axios = axios
Vue.use(BootstrapVue).use(IconsPlugin)
Vue.config.productionTip = false

new Vue({
    router,
    store,
    axios,
    render: h => h(App)
}).$mount('#app')