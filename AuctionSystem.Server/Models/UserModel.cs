using AuctionSystem.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Models
{
    /// <summary>
    /// Model for easier deserialization
    /// </summary>
    public class Data : User
    {

    }

    public class Root
    {
        public Data data { get; set; }
    }


}
