export default class Auction {
    constructor(id, title, description, publishedOn, leadingBid, endDate, stateId) {
        this.id = id
        this.title = title
        this.description = description
        this.publishedOn = publishedOn
        this.leadingBid = leadingBid
        this.endDate = endDate
        this.stateId = stateId
    }
}