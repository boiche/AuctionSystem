using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.Server.Controllers
{
    public abstract class BaseController : Controller
    {
        protected AuctionSystemContext context;
        protected ISession Session { get => HttpContext.Session; }
    }
}
