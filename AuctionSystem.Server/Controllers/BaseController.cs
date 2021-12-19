using AuctionSystem.Server.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.Server.Controllers
{
    public abstract class BaseController : Controller
    {
        public UserCredentials UserCredentials { get; set; }
    }
}
