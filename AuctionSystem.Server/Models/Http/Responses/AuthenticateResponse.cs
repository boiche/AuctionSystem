using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuctionSystem.Server.Models.Http.Responses
{
    public class AuthenticateResponse : ActionResult
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Username = user.Username;
            Token = token;
        }
    }
}
