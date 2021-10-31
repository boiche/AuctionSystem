using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid AuctionId { get; set; }
        public string Description { get; set; }

        public virtual Auction Auction { get; set; }
        public virtual User User { get; set; }
    }
}
