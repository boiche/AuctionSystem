using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSystem.Server.Controllers
{
    public abstract class BaseController : Controller
    {
        protected AuctionSystemContext context;
        protected BaseController() { }
        protected BaseController(AuctionSystemContext context)
        {
            this.context = context;
        }
    }
}
