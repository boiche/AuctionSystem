<template>
    <div class="home">
        <h1>Active auctions</h1>
        <span v-for="auction in auctions.auctions" v-bind:key="auction.id">
            <div class="wrapper" v-if="auction.stateId == 0">
                <AuctionLink :id="auction.id" :title="auction.title"></AuctionLink>
            </div>
        </span>
        <h1>Expired auctions</h1>
        <span v-for="auction in auctions.auctions" v-bind:key="auction.id">
            <div class="wrapper" v-if="auction.stateId != 0">
                <AuctionLink :id="auction.id" :title="auction.title"></AuctionLink>
            </div>
        </span>
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