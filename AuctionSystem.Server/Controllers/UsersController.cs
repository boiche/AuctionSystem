using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuctionSystem.Server.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService service;
        public UsersController(AuctionSystemContext context, IUserService service)
        {
            this.service = service;
            this.service.SetContext(context);
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
                var response = this.service.Login(request);
                if (response.Id != null && response.Id != Guid.Empty)
                {
                    return Ok(response);
                }
                else return Unauthorized(response);
            }
            catch (NullReferenceException)
            {
                //TODO: log exception
                throw;
            }
        }
    }
}
