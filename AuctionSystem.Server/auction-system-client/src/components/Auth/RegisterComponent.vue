<template>
  <div class="mx-auto" style="width: 50%; border: double; padding: 25px">
    <h1>Register</h1>
    <b-form @submit="onSubmit" @reset="onReset" v-if="show">
      <b-form-group id="input-group-1" label="Names:" label-for="input-1" class="row">
        <b-form-input id="input-first-name" v-model="form.FirstName" type="text" class="col" required></b-form-input>
        <b-form-input id="input-second-name" v-model="form.SecondName" type="text" class="col"></b-form-input>
        <b-form-input id="input-third-name" v-model="form.ThirdName" type="text" class="col" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-2" label="Username:" label-for="input-2">
        <b-form-input id="input-username" v-model="form.Username" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-3" label="Additional information:" label-for="input-3">
        <b-form-input id="input-email" v-model="form.Email" required></b-form-input>
        <b-form-input id="input-phone" v-model="form.Phone" required></b-form-input>
        <b-form-input id="input-address" v-model="form.Address" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-4" label="Your password:" label-for="input-4">
        <b-form-input id="input-password" v-model="form.Password" type="password" required></b-form-input>
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
        FirstName: '',
        SecondName: '',
        ThirdName: '',
        FullName: '',
        Username: '',
        Password: '',
        Email: '',
        Phone: '',
        Address: '',
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
