<template>
    <div id="app">
        <div id="nav">
                <b-navbar id="navbar" type="light" variant="light">
                    <ul class="nav navbar-nav navbar-right">
                        <li class="nav-item"><router-link class="nav-link" to="/">Home</router-link></li>
                        <li class="nav-item"><router-link class="nav-link" to="/about">About</router-link></li>
                        <li class="nav-item" v-if="loggedIn"> <router-link class="nav-link" to="/Direct">Chats</router-link></li>
                        <li class="nav-item"><router-link class="nav-link" to="/login">{{loggedIn ? 'Profile' : 'Login'}}</router-link></li>
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
</template>

<script type="text/javascript">
    import LogoutButton from './components/Auth/LogoutButton.vue'
    export default {
        methods: {
            open() {
                try {
                    var user = localStorage.getItem("user");
                } catch (e) {
                    console.log('Please log in in order to use this functionality');
                }
                this.$store.state.signalr.connection.invoke("SendMessage", user, '{ "Username": "some idiot" }', 'some message').catch(function (err) {
                    return console.error(err.toString());
                });
            }
        },
        data() {
            return {
                chatBoxes: JSON.parse(localStorage.getItem('chatBoxes')),
                
            }
        },
        computed: {
            loggedIn() {
                return this.$store.state.auth.status.loggedIn
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
        height: 50px;
        margin-top: 30px;
        padding: 13px;
    }
</style>