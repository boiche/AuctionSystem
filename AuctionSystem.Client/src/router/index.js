import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Direct from '../views/Direct.vue'
import Login from '../views/Auth/Login.vue'
import Register from '../views/Auth/Register.vue'
import UserProfile from '../views/UserProfile.vue'
import Auction from '../views/Auction.vue'
import Admin from '@/components/Admin/AdminIndex.vue'

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/Direct',
        name: 'Direct',
        component: Direct,
    },
    {
        path: '/About',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    },    
    {
        path: '/Login',
        name: 'Login',
        component: Login
    },
    {
        path: '/Register',
        name: 'Register',
        component: Register
    },
    {
        path: '/userProfile/:username',
        name: 'userProfile',
        component: UserProfile
    },
    {
        path: '/Auction/:id',
        name: 'Action',
        component: Auction
    },
    {
        path: '/Admin',
        name: 'Admin',
        component: Admin
    }
]

const router = new VueRouter({
    mode: 'history',
    routes
})


router.beforeEach((from, to, next) => {
    if (from.meta.requiresAuth) {
        if (!window.localStorage.getItem('user')) {
            next({
                name: 'login'
            })
        } else {
            next()
        }
    } else {
        next()
    }
})

export default router