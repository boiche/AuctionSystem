using AuctionSystem.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AuctionSystem.Server.Models.Http.Responses
{
    public class AuthenticateResponse : ActionResult
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
        public DateTime? BanDate { get; set; }
        public string BanReason { get; set; }
        public bool WronCredentials { get; set; }

        public AuthenticateResponse(DateTime banDate, string banReason)
        {
            BanDate = banDate;
            BanReason = banReason;
        }
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Username = user.Username;
            Token = token;
            Role = user.Role ?? 1;
        }
        public AuthenticateResponse(Guid userId, bool wrongCredentials)
        {
            this.WronCredentials = wrongCredentials;
            this.Id = userId;
        }
    }
}
