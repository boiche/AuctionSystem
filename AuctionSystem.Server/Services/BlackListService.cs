using AuctionSystem.Server.Services.Interfaces;
using System.Linq;

namespace AuctionSystem.Server.Services
{
    public class BlackListService : BaseService, IBlackListService
    {
        public BlackListService()
        {

        }

        public bool IsInList(string iPAddress)
        {
            return context.BlackListIps.FirstOrDefault(x => x.Ipaddress == iPAddress) != null;
        }
    }
}
