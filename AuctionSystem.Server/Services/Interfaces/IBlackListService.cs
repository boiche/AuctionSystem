using System.Net;

namespace AuctionSystem.Server.Services.Interfaces
{
    public interface IBlackListService : IService
    {
        bool IsInList(IPAddress iPAddress);
    }
}
