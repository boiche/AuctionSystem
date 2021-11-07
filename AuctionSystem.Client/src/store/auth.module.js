import AuthService from '../services/authService.js'

const user = JSON.parse(localStorage.getItem('user'))
const initialState = user ? { status: { loggedIn: true }, user } : { status: { loggedIn: false } }

export const auth = {
    namespaced: true,
    state: initialState,
    actions: {
        login({ commit }, user) {
            return AuthService.loginUser(user).then(user => {
                commit('loginSuccess', user)
                return Promise.resolve(user)
            },
                error => {
                    commit('loginFailure')
                    return Promise.reject(error)
                })
        },
        logout({ commit }) {
            AuthService.logout(user)
            commit('logout')
        },
        register({ commit }, user) {
            return AuthService.registerUser(user).then(response => {
                commit('registerSuccess')
                return Promise.resolve(response.data)
            },
                error => {
                    commit('registerFailure')
                    return Promise.reject(error)
                })
        }
    },
    mutations: {
        loginSuccess(state, user) {
            state.status.loggedIn = true
            state.user = user
        },
        loginFailure(state) {
            state.status.loggedIn = false
            state.user = null
        },
        registerSuccess(state) {
            state.status.loggedIn = false
        },
        registerFailure(state) {
            state.status.loggedIn = false
        },
        logout(state) {
            state.status.loggedIn = false
            state.user = null
        }
    }
}