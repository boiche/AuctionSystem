using AuctionSystem.Data.Model;
using AuctionSystem.Server.Services.Interfaces;

namespace AuctionSystem.Server.Services
{
    public abstract class BaseService : IService
    {
        protected AuctionSystemContext context;
    }
}