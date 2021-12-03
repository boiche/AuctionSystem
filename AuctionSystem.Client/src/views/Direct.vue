<template>
    <div class="container">
        <ChatBox
                 :current-user="currentUser"
                 :recipient="recipient"></ChatBox>
    </div>
</template>

<script>
    import ChatBox from '@/components/chatBox.vue'
    export default {
        name: 'Direct',
        components: {
            ChatBox
        },
        data() {
            return {
                currentUser: localStorage.getItem('user'),
                recipient: 'Ivan Petrov'
            }
        },
        methods: {
            getUser() {
                return this.$store.state.auth.user
            }
        },
        mounted() {
            this.$store.state.signalr.connection.invoke("BeginChat", this.currentUser, this.recipient).catch(function (err) {
                return console.error(err);
            });
        }
    }
</script>