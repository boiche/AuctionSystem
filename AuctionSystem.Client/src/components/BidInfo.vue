<template>
    <div>
        <div>{{user}}: {{amount}}$ on {{datetime}}</div>
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