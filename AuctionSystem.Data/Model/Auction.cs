using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class Auction
    {
        public Auction()
        {
            Bids = new HashSet<Bid>();
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Description { get; set; }
        public int? StateId { get; set; }

        public virtual AuctionState State { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
