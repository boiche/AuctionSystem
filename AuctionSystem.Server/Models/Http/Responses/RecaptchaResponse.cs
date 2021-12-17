using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Models.Http.Responses
{
    public class RecaptchaResponse
    {
        public bool Success { get; set; }
        public string Hostname { get; set; }
    }
}
