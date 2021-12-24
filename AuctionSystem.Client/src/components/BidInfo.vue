<template>
    <div>
        <div id="bid"><span id="user">{{user}}: </span> <span id="amount">{{amount}}$</span> on <span id="time">{{datetime}}</span></div>
    </div>
</template>

<script>
    export default {
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
</style>