using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionSystem.Data.Model
{
    public partial class Auction
    {
        [NotMapped]
        public Bid LeadingBid { get; set; }
    }
}
