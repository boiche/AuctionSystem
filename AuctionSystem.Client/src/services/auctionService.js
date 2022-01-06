import axios from 'axios'
import moment from 'moment'
const API_URL = 'https://localhost:44305/Auction'

class AuctionService {
    async getRecentAuctions() {
        var result;
        await axios({
            url: API_URL + '/Recent',
            method: 'GET'
        }).then(function (response) {
            result = response.data;
        });
        return result;
    }

    async getAuctions() {
        var result;
        await axios.get(API_URL + '/GetAll').then(function (response) {
            result = response.data.auctions;
            result.map(x => x.publishedOn = moment(x.publishedOn).format('DD/MM/YYYY HH:mm:SS'));
            console.log(result);
        });
        return result;
    }

    async getById(id) {
        var result;
        await axios.get(API_URL + '/GetById', { params: { id: id }}).then(function (response) {
            result = response.data;
        });
        return result;
    }

    async getBids(auctionId, all = false) {
        var result;
        await axios.get('https://localhost:44305/Auction/GetBids', { params: { auctionId: auctionId, all: all } }).then(function (response) {
            result = response.data.bids;
            result.map(x => x.createdOn = moment(x.createdOn).format('DD/MM/YYYY HH:mm:SS'));
        });
        return result;
    }

    async placeBid(id, amount) {
        var form = new FormData();
        form.append('auctionId', id);
        form.append('amount', amount);
        await axios.post(API_URL + '/Bid', form);
    }

    async createAuction(title, description, publishedOn, endDate, minBid) {
        var form = new FormData();
        form.append('title', title);
        form.append('description', description);
        form.append('publishedOn', new Date().toISOString());
        form.append('endDate', endDate);
        form.append('minBid', minBid);
        await axios.post(API_URL + '/CreateAuction', form);
    }

    editAuction(auction) {
        axios.post(API_URL + '/EditAuction', auction);
    }

    async suspendAuction(auctionId) {
        var form = new FormData();
        form.append('auctionId', auctionId);
        await axios.post(API_URL + '/Suspend', form);
    }

    async finishAuction(auctionId) {
        await axios.post(API_URL + '/Finish', auctionId);
    }
}

export default new AuctionService()