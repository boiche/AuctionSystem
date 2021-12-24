<template>
    <div class="home">
        <h1>Active auctions</h1>
        <div class="wrapper" v-for="auction in auctions.auctions" v-bind:key="auction.id">
            <AuctionLink :id="auction.id" :title="auction.title"></AuctionLink>
        </div>
        
    </div>
</template>

<script>
    import auctionService from '@/services/auctionService.js';
    import AuctionLink from '@/components/auctionLink.vue';
    export default {
        components: {
            AuctionLink
        },
        data() {
            return {
                auctions: []
            }
        },
        async created() {
            this.auctions = await auctionService.getRecentAuctions();
            console.log('test');
            console.log(this.auctions);
        }
    }
</script>

<style>
    .wrapper {
        display: inline-block;
        margin: 10px;
        background: #eee;
        width: 30%;
        border: 2px solid #212121;
        border-radius: 4px;
        padding: 10px;
        color: #2c3e50;
    }
    #auctionPic {
        width: 80%;
        padding: 10px;
    }
</style>