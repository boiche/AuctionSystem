using AuctionSystem.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Services
{
    public class BlackListService : BaseService, IBlackListService
    {
        public BlackListService()
        {

        }

        public bool IsInList(IPAddress iPAddress)
        {           
            return context.BlackListIps.Select(x => x.Ipaddress).Contains(BitConverter.ToInt32(iPAddress.GetAddressBytes(), 0));
        }
    }
}
