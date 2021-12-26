import AuthService from '../services/authService.js'

const user = JSON.parse(localStorage.getItem('user'))
const initialState = user ? {
    status: {
        loggedIn: true,
        admin: user.role == 3
    },
    user
} : {
        status: {
        loggedIn: false,
        admin: false
    }
}

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
            state.admin = user.role == 3
        },
        loginFailure(state) {
            state.status.loggedIn = false
            state.user = null
            state.admin = false
        },
        registerSuccess(state) {
            state.status.loggedIn = false
            state.admin = false
        },
        registerFailure(state) {
            state.status.loggedIn = false
            state.admin = false
        },
        logout(state) {
            state.status.loggedIn = false
            state.user = null
            state.admin = false
        }
    }
}