<template>
    <a :href="link" append>
        <h2>{{title}}</h2>
        <!--<img :src="'../../public/pictures/'+Auction.picture.toString()" />-->
        <img id="auctionPic" src="@/assets/pictures/auction.jpg" />
    </a>
</template>

<script>
    export default {
        data() {
            return {
                link: '',
                pictureUrl: '',
            }
        },
        props: {
            title: String,
            id: String
        },
        created() {
            this.link = window.location.origin + '/Auction/' + this.id;
        }
    }
</script>