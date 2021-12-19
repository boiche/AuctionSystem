using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using System;
using Newtonsoft.Json;

namespace AuctionSystem.Server.Controllers
{
    public class AuctionController : BaseController
    {
        private readonly AuctionSystemContext context;
        public AuctionController(AuctionSystemContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public JsonResult Recent()
        {
            var auctions = context.Auctions.OrderByDescending(x => x.PublishedOn).Take(9).ToList();
            return Json(new { auctions });
        }

        [HttpGet]
        public JsonResult GetById(string id)
        {
            Guid.TryParse(id, out Guid auctionId);
            Auction auction = context.Auctions.FirstOrDefault(x => x.Id == auctionId);
            auction.LeadingBid = context.Bids.Where(x => x.AuctionId == auction.Id).OrderByDescending(x => x.Amount).FirstOrDefault();
            auction.LeadingBid.Auction = null;
            return Json(new { auction });
        }

        [HttpPost]
        [ActionName("Bid")]
        public ActionResult Bid([FromForm]Guid auctionId, [FromForm]decimal amount)
        {
            if (context.Bids.Where(x => x.AuctionId == auctionId).OrderByDescending(x => x.Amount).FirstOrDefault().Amount >= amount)
            {
                return BadRequest();
            }
            Bid bid = new Bid()
            {
                Amount = amount,
                AuctionId = auctionId,
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                UserId = (HttpContext.Items["User"] as User).Id             
            };
            context.Bids.Add(bid);
            if (context.SaveChanges() > 0)
                return Ok();
            else
                return Problem($"Unsuccessfull creation of new bid");
        }

        [HttpPost]
        public IActionResult CreateAuction([FromBody] Auction auction)
        {
            context.Auctions.Add(auction);
            context.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult EditAuction([FromBody]Auction auction)
        {
            var toUpdate = context.Auctions.FirstOrDefault(x => x.Id == auction.Id);
            MapperConfiguration configuration = new MapperConfiguration(config => config.CreateMap<Auction, Auction>());
            var mapper = configuration.CreateMapper();
            toUpdate = mapper.Map<Auction>(auction);
            context.SaveChanges();
            return View();
        }
    }
}
