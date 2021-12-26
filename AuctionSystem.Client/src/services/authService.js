import axios from 'axios'
import router from '../router/index.js'
import sha256 from 'js-sha256'
const API_URL = 'https://localhost:44305/Users'

class AuthService {
    registerUser(data) {
        data.password = sha256(data.password)
        return axios.post(API_URL + '/Register', data).then(function () {
            router.push('/Login')
        })
    }

    loginUser(data) {
        const dataForm = {
            username: data.username,
            password: sha256(data.password)
        }
        return axios.post(API_URL + '/Login', dataForm).then(function (response) {
            if (response.data.token) {
                localStorage.setItem('user', JSON.stringify(response.data))
            }

            return response.data
            // router.push('/')
        })
    }

    logout() {
        localStorage.removeItem('user');
        router.push('/Login')
    }

    async getUser(username) {
        var result;
        await axios.get(API_URL + '/GetUser', { params: { username: username } }).then(function (response) {
            result = response.data;
        });
        return result;
    }

    async getUsers() {
        var result;
        await axios.get(API_URL + '/GetUsers').then(function (response) {
            result = response.data.users;
        });
        return result;
    }

    banUser(banRequest) {
        axios.post(API_URL + '/BanUser', banRequest);
    }
}

export default new AuthService()