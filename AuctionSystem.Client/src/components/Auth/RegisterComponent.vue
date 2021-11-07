<template>
    <div class="mx-auto" style="width: 50%; border: double; padding: 25px">
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

            <b-button type="submit" variant="primary" class="m-1">Submit</b-button>
            <b-button type="reset" variant="danger">Reset</b-button>
        </b-form>
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
      this.form.email = ''
      this.form.name = ''
      this.form.food = null
      this.show = false
      this.$nextTick(() => {
        this.show = true
      })
    }
  }
}
</script>