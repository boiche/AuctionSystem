import axios from 'axios'
import router from '../router/index.js'
const API_URL = 'https://localhost:44305/Users'

class AuthService {
  registerUser (data) {
    axios.post(API_URL + '/Register', { data }).then(function () {
      console.log('Front-end message: user ' + data.Username + 'registered successfully!')

      router.push('/Login')
    })
  }

  loginUser (data) {
    const dataForm = {
      username: data.username,
      password: data.password
    }
    return axios.post(API_URL + '/Login', dataForm).then(function (response) {
      if (response.data.token) {
        localStorage.setItem('user', JSON.stringify(response.data))
      }

      return response.data
      // router.push('/')
    })
  }

  logout () {
    localStorage.removeItem('user')
  }
}

export default new AuthService()
