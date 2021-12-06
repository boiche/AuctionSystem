<template>
    <div>
        <div class="card card-container">
            <h1>Register</h1>
            <b-form @submit="onSubmit" @reset="onReset" v-if="show">
                <b-form-group id="input-group-1" label="Names:" label-for="input-1">
                    <b-form-input id="input-first-name" v-model="form.firstName" type="text" placeholder="First name" required></b-form-input>
                    <b-form-input id="input-second-name" v-model="form.secondName" type="text" placeholder="Second name" required></b-form-input>
                    <b-form-input id="input-third-name" v-model="form.thirdName" type="text" placeholder="Third name" required></b-form-input>
                </b-form-group>

                <b-form-group id="input-group-2" label="Additional information:" label-for="input-2">
                    <b-form-input id="input-address" v-model="form.address" placeholder="Address" required></b-form-input>
                    <b-form-input id="input-email" v-model="form.email" placeholder="Email" type="email" required></b-form-input>
                    <b-form-input id="input-phone" v-model="form.phone" placeholder="Phone" type="tel" required></b-form-input>
                </b-form-group>

                <b-form-group id="input-group-2" label="Your username:" label-for="input-2">
                    <b-form-input id="input-username" v-model="form.username" placeholder="Username" required></b-form-input>
                </b-form-group>

                <b-form-group id="input-group-2" label="Your password:" label-for="input-2">
                    <b-form-input id="input-password" v-model="form.password" placeholder="Password" type="password" required></b-form-input>
                </b-form-group>

                <b-button type="submit" variant="primary" class="button">Submit</b-button>
                <b-button type="reset" variant="danger" class="button">Reset</b-button>
            </b-form>
        </div>
        <p>Already have an account? <router-link to="/login">Login</router-link></p>
    </div>
</template>

<script>
import AuthService from '../../services/authService.js'
export default {
  data () {
    return {
      form: {
        firstName: '',
        secondName: '',
        thirdName: '',
        address: '',
        email: '',
        phone:'',
        username: '',
        password: '',
        roles: ''
      },
      show: true
    }
  },
  methods: {
    onSubmit (event) {
      event.preventDefault()
      AuthService.registerUser(this.form)
    },
    onReset (event) {
        event.preventDefault()
        this.form.firstName = ''
        this.form.secondName = ''
        this.form.thirdName = ''
        this.form.address = ''
        this.form.email = ''
        this.form.phone = ''
        this.form.username = ''
        this.form.password = ''
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    }
  }
}
</script>
<style>
    .card-container.card {
        width: 50%;
        margin-left: 23%;
        padding: 40px 2%;
    }
    .card {
        background-color: #f7f7f7;
        padding: 20px;
        padding-top: 0;
        margin: 0 auto 25px;
        margin-top: 20px;
        -moz-border-radius: 2px;
        -webkit-border-radius: 2px;
        border-radius: 2px;
        -moz-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        -webkit-box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
        box-shadow: 0px 2px 2px rgba(0, 0, 0, 0.3);
    }
    .button {
        margin-top: 15px;
        margin-right:15px;
    }
</style>