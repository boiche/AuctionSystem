using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Services.Interfaces;
using AuctionSystem.Server.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace AuctionSystem.Server.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService service;
        private readonly string lockoutKey = "lockout";
       
        public UsersController(AuctionSystemContext context, IUserService service)
        {
            this.service = service;
            this.service.SetContext(context);
            this.context = context;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest register)
        {
            if (service.Register(register))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult Login([FromBody] AuthenticateRequest request)
        {
            try
            {
                if (!request.Checked)
                {
                    return Unauthorized("Please check I'm not a robot");
                }
                request.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var response = this.service.Login(request);
                
                if (response.WronCredentials)
                {
                    return Unauthorized($"Invalid email or password");
                }
                if (response.Id != null && response.Id != Guid.Empty)
                {
                    TempData.Remove($"{lockoutKey}{request.Username}");
                    return Ok(response);
                }
                else if (response.BanDate.HasValue)
                    return Unauthorized($"You're banned until: {response.BanDate} for reason: {response.BanReason}");

                return Unauthorized($"Invalid email or password");

            }
            catch (NullReferenceException)
            {
                //TODO: log exception
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUser")]
        public JsonResult GetUser(string username)
        {
            try
            {
                User user = context.Users.FirstOrDefault(x => x.Username == username);
                return Json(new { user });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUsers")]
        public JsonResult GetUsers()
        {
            try
            {
                var users = context.Users.Where(x => x.Role != (int)Roles.Admin).ToList();
                return Json(new { users });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ActionName("BanUser")]
        public ActionResult BanUser([FromBody]BanRequest banRequest)
        {
            if (service.Ban(banRequest))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [ActionName("UnbanUser")]
        public ActionResult UnbanUser([FromForm]Guid userId)
        {
            if (service.Unban(userId))
                return Ok();
            else
                return BadRequest();
        }
    }
}
