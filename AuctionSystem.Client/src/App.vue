<template>
    <div id="app">
        <div id="content-wrap">
            <div id="nav">
                <b-navbar id="navbar" type="light" variant="light">
                    <ul class="nav navbar-nav navbar-right">
                        <li class="nav-item"><router-link class="nav-link" to="/">Home</router-link></li>
                        <li class="nav-item"><router-link class="nav-link" to="/about">About</router-link></li>
                        <li class="nav-item"><router-link class="nav-link" to="/login">{{loggedIn ? 'Profile' : 'Login'}}</router-link></li>
                        <li class="nav-item" v-if="admin"><router-link class="nav-link" to="/Admin">Admin</router-link></li>
                        <li class="nav-item" v-if="loggedIn"><LogoutButton></LogoutButton></li>
                    </ul>
                </b-navbar>
            </div>
            <router-view />
            <footer class="footer">
                <div class="content has-text-centered">
                    <p>{{ new Date().getFullYear() }} - <strong>Auction System</strong></p>
                </div>
            </footer>
         </div>
    </div>
</template>

<script type="text/javascript">
    import LogoutButton from './components/Auth/LogoutButton.vue'

    export default {
        computed: {
            loggedIn() {
                return this.$store.state.auth.status.loggedIn
            },
            admin() {
                return this.$store.state.auth.user?.role == 3
            }
        },
        components: {
            LogoutButton
        }
    }
</script>

<style>
    #app {
        font-family: Avenir, Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        position: relative;
        min-height: 100vh;
    }
    #content-wrap {
        padding-bottom: 5rem;
    }
    #nav a {
        font-weight: bold;
        color: #2c3e50;
    }

    #nav a.router-link-exact-active {
            color: #42b983;
    }

    footer {
        background-color: #42b983;
        width: 100%;
        padding: 13px;
        position: absolute;
        bottom: 0;
        height: 3rem;
    }
</style>