<template>
    <div>
        <div id="bid">
            <span id="user">
                <router-link class="nav-link userLink" :to="link">
                    {{user}}:
                </router-link>
            </span> 
            <span id="amount">{{amount}}$</span> on 
            <span id="time">{{datetime}}</span>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                link: '/userProfile/' + this.user
            }
        },
        props: {
            user: String,
            amount: Number,
            datetime: String
        }
    }
</script>
<style>

    #bid{
        font-size: 20px;
    }
    #amount{
        font-weight: bold;
    }
    #time {
        font-style: italic;
    }
    .userLink {
        display: inline !important;
    }
</style>