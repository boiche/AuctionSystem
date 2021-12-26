<template>
    <div>
        <div class="card card-container">
            <img id="profile-img" src="//ssl.gstatic.com/accounts/ui/avatar_2x.png" class="profile-img-card" />
            <form name="form" id="loginForm" @submit.prevent="handleLogin">
                <div class="form-group">
                    <label>Username</label>
                    <input v-model="user.username" type="text" class="form-control" name="username" required />
                </div>
                <div class="form-group">
                    <label>Password</label>
                    <input v-model="user.password" type="password" class="form-control" name="password" required />
                </div>
                <div class="form-group">
                    <ReCaptcha @validate="validate"></ReCaptcha>
                </div>
                <div class="form-group">
                    <button class="btn btn-primary btn-block" :disabled="loading">
                        <span v-show="loading" class="spinner-border spinner-border-sm"></span>
                        <span>Login</span>
                    </button>
                </div>
                <div class="form-group">
                    <div v-if="message" class="alert alert-danger" role="alert">{{message}}</div>
                </div>
            </form>
        </div>
        <p>Don't have an account yet? <router-link to="/register">Sign up</router-link></p>
       
    </div>
</template>

<script>
    import User from '../../models/user.js'
    import ReCaptcha from '@/components/Auth/ReCaptcha.vue'
    export default {
        name: 'Login',
        components: {
            ReCaptcha
        },
        data() {
            return {
                user: new User('', ''),
                loading: false,
                message: '',
                validateRecaptcha: false
            }
        },
        computed: {
            loggedIn() {
                return this.$store.state.auth.status.loggedIn
            },
            admin() {
                console.log('admin')
                return this.$store.state.auth.status.admin
            }
        },
        created() {
            if (this.loggedIn) {
                this.$router.push({ name: 'userProfile', params: { username: this.$store.state.auth.user.username } })
            }
        },
        methods: {
            validate(success) {
                this.validateRecaptcha = success
            },
            handleLogin() {
                this.loading = true
                if (this.user.username && this.user.password) {
                    this.$store.dispatch('auth/login', this.user).then(
                        () => {
                            console.log('user logged in successfully. check local storage vor key user');
                            this.$router.push('/')
                            //TODO: navigate to content on successful login (logged in index page or user ptofile page)
                            //this.$router.push({ name: 'userProfile', params: { username: this.$store.state.auth.user.username } })
                        },
                        error => {
                            this.loading = false
                            this.message =
                                (error.response && error.response.data) ||
                                error.message ||
                                error.toString()
                        }
                    )
                }
            }
        }
    }
</script>

<style scoped>
    label {
        display: block;
        margin-top: 10px;
    }

    .card-container.card {
        width: 33%;
        margin-left: 33%;
        padding: 40px;
    }

    .card {
        background-color: #f7f7f7;
        padding: 20px;
        margin: 0 auto 25px;
        margin-top: 50px;
        -moz-border-radius: 2px;
        -webkit-border-radius: 2px;
        border-radius: 2px;
        -moz-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        -webkit-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
    }

    .profile-img-card {
        width: 40%;
        margin:10px;
        margin-left: 30%;
        border-radius: 50%;
    }
    button{
        margin-top: 20px;
    }
</style>