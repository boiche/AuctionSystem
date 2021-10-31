using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class AuctionState
    {
        public AuctionState()
        {
            Auctions = new HashSet<Auction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Auction> Auctions { get; set; }
    }
}
