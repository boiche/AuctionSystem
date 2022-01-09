using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AutoMapper;
using System;
using System.Data.Entity;
using AuctionSystem.Server.Utils;
using System.Collections.Generic;
using MimeKit;
using MailKit.Net.Smtp;

namespace AuctionSystem.Server.Controllers
{
    public class AuctionController : BaseController
    {
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
        public JsonResult GetAll()
        {
            var auctions = context.Auctions.OrderByDescending(x => x.PublishedOn).ToList();
            auctions.ForEach(x =>
            {
                x.State = context.AuctionStates.FirstOrDefault(y => y.Id == x.StateId);

                if (x.State != null)
                    x.State.Auctions = null;
            });
            return Json(new { auctions });
        }

        [HttpGet]
        public JsonResult GetBids(Guid auctionId, bool all = false)
        {
            var allBids = context.Bids.Where(x => auctionId == x.AuctionId).OrderByDescending(x => x.CreatedOn);
            List<Bid> bids;
            if (!all)
                bids = allBids.Take(5).ToList();
            else
                bids = allBids.ToList();

            bids.ForEach(x =>
            {
                x.User = new User() { Username = context.Users.First(y => y.Id == x.UserId).Username };
                x.Auction = new Auction() { Title = context.Auctions.First(y => y.Id == x.AuctionId).Title };
            });
            return Json(new { bids });
        }

        [HttpGet]
        public JsonResult GetById(string id)
        {
            Guid.TryParse(id, out Guid auctionId);
            Auction auction = context.Auctions.FirstOrDefault(x => x.Id == auctionId);
            auction.LeadingBid = context.Bids.Where(x => x.AuctionId == auction.Id).OrderByDescending(x => x.Amount).FirstOrDefault() ?? new Data.Model.Bid();
            auction.LeadingBid.Auction = null;
            return Json(new { auction });
        }

        [HttpPost]
        [ActionName("Bid")]
        public ActionResult Bid([FromForm] Guid auctionId, [FromForm] decimal amount)
        {
            if (context.Bids.Where(x => x.AuctionId == auctionId).OrderByDescending(x => x.Amount).FirstOrDefault()?.Amount >= amount ||
                context.Auctions.First(x => x.Id == auctionId).StateId != (int)AuctionStates.Active)
            {
                return BadRequest();
            }

            Bid bid = new Bid()
            {
                Amount = amount,
                AuctionId = auctionId,
                CreatedOn = DateTime.Now,
                Id = Guid.NewGuid(),
                //UserId = (HttpContext.Items["User"] as User).Id;
                UserId = Guid.Parse("8ad0eec8-33cf-4360-b6c0-db8c6dc53ffe")
            };
            context.Bids.Add(bid);
            if (context.SaveChanges() > 0)
                return Ok();
            else
                return Problem($"Unsuccessfull creation of new bid");
        }

        [HttpPost]
        public IActionResult EditAuction([FromBody]Auction auction)
        {
            var toUpdate = context.Auctions.FirstOrDefault(x => x.Id == auction.Id);
            if (toUpdate == null)
                return BadRequest();

            //MapperConfiguration configuration = new MapperConfiguration(config => config.CreateMap<Auction, Auction>());
            //var mapper = configuration.CreateMapper();
            //toUpdate = mapper.Map<Auction>(auction);
            toUpdate.Description = auction.Description;
            toUpdate.Title = auction.Title;
            if (context.SaveChanges() > 0)
                return Ok();
            else
                return new EmptyResult();
        }

        [HttpPost]
        [ActionName("CreateAuction")]
        public IActionResult CreateAuction([FromForm]Auction auction)
        {
            auction.Id = Guid.NewGuid();
            auction.StateId = (int)AuctionStates.Active;
            //auction.UserId = (HttpContext.Items["User"] as User).Id;
            auction.UserId = Guid.Parse("8ad0eec8-33cf-4360-b6c0-db8c6dc53ffe");

            context.Auctions.Add(auction);

            if (context.SaveChanges() > 0) return Ok();
            else return BadRequest();
        }

        [HttpPost]
        [ActionName("Suspend")]
        public IActionResult Suspend([FromForm]Guid auctionId)
        {
            var auction = context.Auctions.FirstOrDefault(x => x.Id == auctionId);
            if (auction == null)
                return BadRequest();

            auction.StateId = (int)AuctionStates.Suspended;
            if (context.SaveChanges() > 0)
                return Ok();

            return new EmptyResult();
        }

        [HttpPost]
        [ActionName("Finish")]
        public IActionResult Finish ([FromForm]Guid auctionId)
        {
            var auction = context.Auctions.FirstOrDefault(x => x.Id == auctionId);

            //auctionId идва като null от фронтенда. Тази проверка го заобикаля, но не работи, ако има повече от един изтекъл търг.
            if (auction == null)
                auction = context.Auctions.Where(x => DateTime.Now > x.PublishedOn.AddDays(7) && x.StateId == 0).FirstOrDefault();

            if (auction == null)
                return BadRequest();

            auction.LeadingBid = context.Bids
                                            .Where(x => x.AuctionId == auction.Id)
                                            .OrderByDescending(x => x.Amount)
                                            .FirstOrDefault();
            var winner = context.Users.Where(x => x.Id == auction.LeadingBid.UserId).FirstOrDefault();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Auction System", "auction.system22@gmail.com"));
            message.To.Add(new MailboxAddress(winner.FullName, winner.Email));
            message.Subject = "Auction won";
            message.Body = new TextPart("plain")
            {
                Text = "Congratulations, \n You won the " + auction.Title + " auction with your leading bid (" + auction.LeadingBid.Amount.ToString() + "$)!"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("auction.system22@gmail.com", "AUC123-auc");
                client.Send(message);
                client.Disconnect(true);
            }

            auction.StateId = (int)AuctionStates.Finished;
            if (context.SaveChanges() > 0)
                return Ok();

            return new EmptyResult();
        }
    }
}
