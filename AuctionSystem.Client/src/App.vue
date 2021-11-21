<template>
    <div id="app">
        <div id="nav">
            <router-link to="/">Home</router-link> |
            <router-link to="/about">About</router-link>
            <router-link to="/Register">Register</router-link>
            <router-link to="/Login">Login</router-link>
        </div>
        <router-view />
        <div id="openChatBox" class="btn btn-info" v-on:click="open()">Open chat box</div>
        <div class="row" style="align-self: flex-end">            
        </div>
    </div>
</template>

<script>
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
                chatBoxes: JSON.parse(localStorage.getItem('chatBoxes'))
            }
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

    #nav {
        padding: 30px;
    }

    #nav a {
        font-weight: bold;
        color: #2c3e50;
    }

    #nav a.router-link-exact-active {
        color: #42b983;
    }
</style>