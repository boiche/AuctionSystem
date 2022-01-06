<template>
    <div>
        <div id="wrapper">
            <h1>{{auction.title}}</h1>
            <h2 v-if="auction.stateId == 0">
                <Countdown :date="expiringOn" @onFinish="finish"></Countdown>
            </h2>
            <h2 v-else>
                This auction is no longer active
            </h2>
            <img id="auctionPic" src="@/assets/pictures/auction.jpg" />
            <p style="font-size:20px">{{auction.description}}</p>
            <div class="formInput" v-if="auction.stateId == 0 && loggedIn">
                <input v-model="amount" step="0.01" min="0" type="number" id="usermsg" />
                <div class="btn btn-submit" id="submitmsg" type="submit" value="Bid" v-on:click="placeBid">Bid</div>
            </div>
        </div>
        <div id="wrapper" class="col-4">
            <h1>Recent bids</h1>
            <BidInfo v-for="bid in bids" v-bind:key="bid.id" :user="bid.user.username" :amount="bid.amount" :datetime="bid.createdOn"></BidInfo>
        </div>
    </div>
</template>

<script>
    import moment from 'moment'
    import Countdown from '@/components/Countdown.vue';
    import Auction from '@/models/auction.js'
    import AuctionService from '@/services/auctionService.js'
    import BidInfo from '@/components/BidInfo.vue'

    export default {
        components: { Countdown, BidInfo },
        data() {
            return {
                bids: new Array(),
                auction: null,
                expiringOn: moment(),
                amount: 0,
                refreshBids: null
            }
        },
        async created() {
            var result = await AuctionService.getById(this.$route.params.id);
            this.auction = new Auction(result.auction.id, result.auction.title, result.auction.description, result.auction.publishedOn, result.auction.leadingBid.amount, null, result.auction.stateId, result.auction.minBid);
            this.expiringOn = moment(this.auction.publishedOn).add(7, 'days').format('MM.D.YYYY')
            this.refreshBids = async function refreshBids() {
                this.bids = await AuctionService.getBids(result.auction.id);
            };
            await this.refreshBids();
            this.bids.map(x => {
               // x.createdOn = moment(x.createdOn).format('do.MMM.YY HH:mm');
                console.log(x.createdOn)
            });
        },
        methods: {
            async placeBid() {
                if (this.amount <= this.auction.leadingBid || this.amount < this.auction.minBid) {
                    this.$toastr.w('Your bid is too small');
                    return;
                }
                await AuctionService.placeBid(this.auction.id, this.amount);
                //TODO: show error message when bid was not created (amount was smaller than highest bid)
                this.$toastr.s('Bid placed successfully');
                var result = await AuctionService.getById(this.$route.params.id);
                this.auction = new Auction(result.auction.id, result.auction.title, result.auction.description, result.auction.publishedOn, result.auction.leadingBid.amount, result.auction.stateId);
                this.$router.go()
            },
            finish() {
                AuctionService.finishAuction(this.auction.id)
            }
        },
        computed: {
            loggedIn() {
                return this.$store.state.auth.status.loggedIn
            }
        }
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
