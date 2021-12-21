import axios from 'axios'
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

    async getById(id) {
        var result;
        await axios.get(API_URL + '/GetById', { params: { id: id }}).then(function (response) {
            result = response.data;
        });
        return result;
    }

    async getBids(auctionId) {
        var result;
        await axios.get('https://localhost:44305/Auction/GetBids', { params: { auctionId: auctionId } }).then(function (response) {
            result = response.data.bids;
        });
        return result;
    }

    async placeBid(id, amount) {
        var form = new FormData();
        form.append('auctionId', id);
        form.append('amount', amount);
        await axios.post(API_URL + '/Bid', form);
    }
}

export default new AuctionService()