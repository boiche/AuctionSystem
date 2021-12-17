import axios from 'axios'

export default {
    validate(request) {
        return new Promise((resolve, reject) => {
            axios.post(`https://localhost:44305/Recaptcha/validate`, request)
                .then(response => {
                    if (response.data.hasErrors) {
                        reject(response.data.message)
                    } else {
                        resolve(response.data)
                    }
                })
                .catch(error => {
                    console.log(error);
                })
        })
    }
}