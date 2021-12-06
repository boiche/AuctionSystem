<template>
    <div id="wrapper">
        <h1>Auction.Title</h1>
        <h2>
            <!--<Countdown end="Auction.PublishedOn.getDate()+7"></Countdown>-->
            <Countdown end="12 25, 2021"></Countdown>
        </h2>
        <!--<img :src="'../../public/pictures/'+Auction.picture.toString()" />-->
        <img id="auctionPic" src="../../public/pictures/auction.jpg" />
        <p>Auction.Description</p>
        <div class="formInput">
            <input type="number" id="usermsg" />
            <div class="btn btn-submit" id="submitmsg" type="submit" value="Bid">Bid</div>
        </div>
    </div>
</template>

<script>
    import Countdown from 'vuejs-countdown'

    export default {
        components: { Countdown }
    }
</script>
<style>
    #auctionPic{
        width: 80%;
        padding: 10px;
    }
    #submitmsg {
        background: #ff9800;
        border: 2px solid #e65100;
        color: white;
        padding: 4px 10px;
        font-weight: bold;
        border-radius: 4px;
        cursor: pointer !important;
        width: 20%;
        margin-left: 10px;
    }

    #usermsg {
        flex: 1;
        border-radius: 4px;
        border: 1px solid #ff9800;
        padding-left: 5px;
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
</style>
