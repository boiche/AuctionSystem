using System;

namespace AuctionSystem.Server.Models.Http.Requests
{
    public class BanRequest
    {
        public Guid UserId { get; set; }
        public DateTime BanDate { get; set; }
        public string BanReason { get; set; }
    }
}
