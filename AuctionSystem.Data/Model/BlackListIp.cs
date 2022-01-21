using System;
using System.Collections.Generic;

#nullable disable

namespace AuctionSystem.Data.Model
{
    public partial class BlackListIp
    {
        public Guid Id { get; set; }
        public string Ipaddress { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
