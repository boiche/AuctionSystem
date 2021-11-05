using AuctionSystem.Data;
using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.Entity;
using System.Linq;

namespace AuctionSystem.Server.Controllers
{
    public class HomeController : BaseController
    {    
        public HomeController(AuctionSystemContext context) : base(context) { }        
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
