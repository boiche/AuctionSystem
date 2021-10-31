using AuctionSystem.Data;
using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity;
using System.Linq;

namespace AuctionSystem.Server.Controllers
{
    [EnableCors("defaultPolicy")]
    public class HomeController : Controller
    {
        private AuctionSystemContext context;
        public HomeController(AuctionSystemContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public JsonResult Index()
        {
            return Json(new { Response = "OK" });
        }

        [HttpPost]
        public JsonResult IndexPost()
        {
            return Json(new { StateID = 5 });
        }

        [HttpPost]
        public JsonResult IndexPostArgument()
        {
            string message = Request.Query["someArgument"].ToString();
            Auction state = context.Auctions.FirstOrDefault();
            Data.Model.User user = context.Users.FirstOrDefault(x => x.Id == state.UserId);
            return Json(new { StateID = state });
        }
    }
}
