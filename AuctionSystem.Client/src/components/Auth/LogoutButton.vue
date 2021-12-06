<template>
    <button class="btn btn-danger btn-block"
            @click="logout">
        Log Out
    </button>
</template>

<script>
    export default {
        name: 'LogoutButton',
        methods: {
            logout() {
                this.$store.dispatch('auth/logout').then(
                    () => {
                        localStorage.removeItem('user')

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
        },
    };
</script>