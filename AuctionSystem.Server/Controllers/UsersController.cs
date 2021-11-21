using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models;
using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Models.Http.Responses;
using AuctionSystem.Server.Services;
using AuctionSystem.Server.Services.Interfaces;
using AuctionSystem.Server.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Pipelines;
using System.Text;

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
                if (response.Id != null && response.Id != Guid.Empty) return Ok(response);
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
