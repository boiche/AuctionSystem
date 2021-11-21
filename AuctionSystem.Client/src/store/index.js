import Vue from 'vue'
import Vuex from 'vuex'
import { auth } from './auth.module.js'
import { signalr } from './chat.module.js'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
      auth,
      signalr
  }
})