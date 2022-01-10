using AuctionSystem.Server.Models.Http.Responses;
using AuctionSystem.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Utils
{
    public class BlackListMiddleware
    {
        private readonly RequestDelegate _next;

        public BlackListMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IBlackListService blackListService, Data.Model.AuctionSystemContext auctionSystemContext)
        {
            blackListService.SetContext(auctionSystemContext);
            IPAddress clientRemoteAddress = context.Connection.RemoteIpAddress;            
            if (blackListService.IsInList(clientRemoteAddress))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "text/html";
                await context.Response.CompleteAsync();
            }
            else
                await _next(context);
        }
    }
}
