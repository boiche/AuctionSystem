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
    }
}
