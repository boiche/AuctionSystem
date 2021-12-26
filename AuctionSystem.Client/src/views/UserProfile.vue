<template>
    <div>
        <div id="wrapper">
            <h1>{{userPage.fullName}} ({{userPage.username}})</h1>
            <h3>{{userPage.email}}</h3>
            <h3>{{userPage.phone}}</h3>
        </div>
        <!--IF THIS IS ANOTHER USER'S PROFILE-->
        <div id="showChat" v-if="isSameUser != true">
            <button id="messageBtn" class="bt" v-on:click="beginChat">Message {{userPage.username}}</button>
            <ChatBox :current-user="currentUser"
                     :recipient="userPage"
                     :visible="!isHiddenMsg"
                     v-if="!isHiddenMsg"></ChatBox>
        </div>
        <!--IF THIS IS YOUR PROFILE-->
        <div v-if="isSameUser">
            <button id="auctionBtn" class="bt" v-on:click="isHiddenA = !isHiddenA">New Auction</button>
            <div id="showCreate" v-if="!isHiddenA">
                <CreateAuction></CreateAuction>
            </div>
        </div>
    </div>
</template>

<script>
    import ChatBox from '@/components/chatBox.vue'
    import CreateAuction from '@/components/createAuction.vue'
    import UserService from '@/services/authService.js'
    import User from '@/models/user.js'

    export default {
        name: 'Direct',
        components: {
            ChatBox, CreateAuction
        },
        data() {
            return {
                currentUser: localStorage.getItem('user'),
                userPage: null,
                isHiddenMsg: true,
                isHiddenA: true,
                isSameUser: false
            }
        },
        methods: {
            getUser() {
                return this.$store.state.auth.user;
            },
            beginChat: function () {
                if (this.isSameUser) {
                    return;
                }                
                this.isHiddenMsg = !this.isHiddenMsg;
                this.$store.state.signalr.connection.invoke("BeginChat", this.currentUser, this.userPage.username).catch(function (err) {
                    return console.error(err);
                });
            }
        },
        async mounted() {
            var result = await UserService.getUser(this.$route.params.username);
            this.userPage = new User(result.user.username, result.user.fullName, result.user.email, result.user.phone);
            this.isSameUser = this.userPage.username === this.$store.state.auth.user.username;
        }
    }
</script>
<style>
    .userMessage {
        display: flow-root;
        background: #221100;
        display: flow-root;
    }

    .messageRowLeft {
        display: flex;
        justify-content: left;
    }

    .messageRowRight {
        display: flex;
        justify-content: right;
    }

    .receiverMessage {
        background: #AAAACC;
        display: inline-block;
    }

    .datetimeHidden {
        display: none;
    }

    .datetime {
        display: inline;
        border-radius: 25px;
        color: black;
        background-color: #eee;
        padding: 10px;
        margin: 5px 2px;
        align-self: center;
    }

    .message {
        border-radius: 25px;
        color: white;
        padding: 10px;
        margin: 5px 0px;
        max-width: 60%;
        /*white-space: initial;*/
        overflow-wrap: break-word;
    }

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
