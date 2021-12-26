using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class Bid
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public Guid AuctionId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual User User { get; set; }
    }
}
