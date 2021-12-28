<template>
    <vue-recaptcha :sitekey="'6LeRPpYdAAAAAFRfHG2qMUBieUCfypk9LExYJLGc'" :loadRecaptchaScript="true" @verify="validate"></vue-recaptcha>
</template>

<script>
    import { VueRecaptcha } from 'vue-recaptcha'
    import Validation from '@/services/authService.js'
    export default {
        components: { VueRecaptcha },
        data() {
            return {
                sitekey: process.env.RECAPTCHA_PUBLIC_KEY
            }
        },
        methods: {
            async validate(response) {
                await Validation.validate({ Response: response }).then(result => {
                    this.$emit('validate', result.isSuccessStatusCode)
                }).catch(error => console.log(error))
            }
        }
    }
</script>