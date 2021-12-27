using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Http;

namespace AuctionSystem.Server.Controllers
{
    [Route("/recaptcha")]
    public class RecaptchaController : BaseController
    {
        private readonly IConfiguration Configuration;
        public RecaptchaController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        [HttpPost]
        [Route("validate")]
        public async System.Threading.Tasks.Task<HttpResponseMessage> ValidateAsync([FromBody] RecaptchaRequest request)
        {
            HttpResponseMessage response = await RecaptchaService.RetrieveResponse(request, Configuration);
            return response;
        }
    }
}
