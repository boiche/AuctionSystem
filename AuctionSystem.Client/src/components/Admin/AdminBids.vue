<template>
    <div>
        <b-table striped hover :items="items" :fields="fields">
            <template #cell(No)="row">
                <div style="width:50px">
                    <div>{{row.index + 1}}</div>
                </div>
            </template>
        </b-table>
    </div>
</template>

<script>
    import AuctionService from '@/services/auctionService.js'

    export default {
        data() {
            return {
                dataArray: null,
                fields: [
                    { key: 'No' },
                    { key: 'amount', label: 'Amount', sortable: true },
                    { key: 'user.username', label: 'User', sortable: true },
                    { key: 'auction.title', label: 'Auction', sortable: true },
                    { key: 'createdOn', label: 'Date', sortable: true }
                ],
            }
        },
        props: {
            auctionId: String,
            bids: Array
        },
        async mounted() {
            console.log(this.bids);
            this.dataArray = await AuctionService.getBids(this.auctionId, true);
        },
        computed: {
            items() {
                if (this.dataArray == undefined) return;
                return this.dataArray;
            }
        }
    }
</script>