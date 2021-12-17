<template>
    <VueRecaptcha :sitekey="'6LeRPpYdAAAAAFRfHG2qMUBieUCfypk9LExYJLGc'" :loadRecaptchaScript="true" @verify="validate" />
</template>

<script>
    import { VueRecaptcha } from 'vue-recaptcha'
    import Validation from '@/services/chatService.js'
    export default {
        components: { VueRecaptcha },
        data() {
            return {
                sitekey: process.env.RECAPTCHA_PUBLIC_KEY
            }
        },
        methods: {
            validate(response) {
                Validation.validate({ Response: response }).then(result => {
                    this.$emit('validate', result.objectResult.success)
                }).catch(error => console.log(error))
            }
        }
    }
</script>