using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Models.Http.Responses;
using System;
using System.Collections.Generic;

namespace AuctionSystem.Server.Services.Interfaces
{
    public interface IUserService
    {
        void SetContext(AuctionSystemContext context);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);

        public bool Register(RegisterRequest newUser);

        public AuthenticateResponse Login(AuthenticateRequest request);
    }
}
