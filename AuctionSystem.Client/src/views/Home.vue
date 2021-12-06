<template>
    <div class="home">
        <h1>Active auctions</h1>
        <router-link to="/Auction">
            <div class="wrapper" v-for="index in 9" :key="index">
                <h2>Auction.Title</h2>
                <!--<img :src="'../../public/pictures/'+Auction.picture.toString()" />-->
                <img id="auctionPic" src="../../public/pictures/auction.jpg" />
            </div>
        </router-link>
    </div>
</template>

<script>
    
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