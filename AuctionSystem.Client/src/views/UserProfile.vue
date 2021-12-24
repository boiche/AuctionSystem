<template>
    <div>
        <div id="wrapper">
            <h1>Firstname Lastname (Username)</h1>
            <h3>username@email.com</h3>
            <h3>0891234567</h3>
        </div>
        <!--IF THIS IS ANOTHER USER'S PROFILE-->
        <button id="messageBtn" class="bt" v-on:click="isHiddenMsg = !isHiddenMsg">Message [username]</button>
        <div id="showChat" v-if="!isHiddenMsg">
            <ChatBox :current-user="currentUser"
                     :recipient="recipient"></ChatBox>
        </div>
        <!--IF THIS IS YOUR PROFILE-->
        <button id="auctionBtn" class="bt" v-on:click="isHiddenA = !isHiddenA">New Auction</button>
        <div id="showCreate" v-if="!isHiddenA">
            <CreateAuction></CreateAuction>
        </div>
    </div>
</template>

<script>
    import ChatBox from '@/components/chatBox.vue'
    import CreateAuction from '@/components/createAuction.vue'
    export default {
        name: 'Direct',
        components: {
            ChatBox, CreateAuction
        },
        data() {
            return {
                currentUser: localStorage.getItem('user'),
                recipient: 'Ivan Petrov',
                isHiddenMsg: true,
                isHiddenA: true
            }
        },
        methods: {
            getUser() {
                return this.$store.state.auth.user;
            }
        },
        mounted() {
            this.$store.state.signalr.connection.invoke("BeginChat", this.currentUser, this.recipient).catch(function (err) {
                return console.error(err);
            });
        }
    }
</script>
<style>
    #wrapper {
        margin: 20px auto;
        background: #eee;
        width: 50%;
        max-width: 100%;
        border: 2px solid #212121;
        border-radius: 4px;
        padding: 20px;
    }
    .bt {
        background: #ff9800;
        border: 2px solid #e65100;
        color: white;
        padding: 4px 10px;
        font-weight: bold;
        border-radius: 4px;
        cursor: pointer !important;
        width: 20%;
        margin-left: 2%;
        margin-bottom: 20px;
    }
</style>
