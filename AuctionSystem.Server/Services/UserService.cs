using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models.Http.Requests;
using AuctionSystem.Server.Models.Http.Responses;
using AuctionSystem.Server.Services.Interfaces;
using AuctionSystem.Server.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace AuctionSystem.Server.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly string lockoutReasonMessage = "Locked out: too many invalid login attempts";

        private readonly JWTSettings _jwtSettings;

        public UserService(IOptions<JWTSettings> appSettings)
        {
            _jwtSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            Thread.Sleep(5000);
            var user = context.Users.SingleOrDefault(x => x.Username == model.Username);

            if (user == null)
                return new AuthenticateResponse(default, true);
            else if (user.Password != model.Password)
            {
                user.LockoutAttempts++;
                if (user.LockoutAttempts >= 5)
                {                    
                    BanRequest banRequest = new BanRequest()
                    {
                        UserId = user.Id,
                        BanDate = DateTime.Now.AddDays(7),
                        BanReason = lockoutReasonMessage
                    };
                    BlackListIp blackListIp = new BlackListIp()
                    {
                        Ipaddress = (int)model.IPAddress,                        
                        Active = true,
                        CreatedOn = DateTime.Now,
                        Id = Guid.NewGuid()
                    };
                    context.BlackListIps.Add(blackListIp);                    
                    Ban(banRequest);
                    return new AuthenticateResponse(banRequest.BanDate, banRequest.BanReason);
                }
                else
                {
                    context.SaveChanges();
                    return new AuthenticateResponse(user.Id, true);
                }
            }
            if (user.BanDate.HasValue)
            {
                if (user.BanDate < DateTime.Now)
                    Unban(user.Id);

                else if (user.BanDate >= DateTime.Now)
                    return new AuthenticateResponse(user.BanDate.Value, user.BanReason);
            }

            var token = GenerateJwtToken(user);

            if (user.LockoutAttempts > 0)
            {
                user.LockoutAttempts = 0;
                context.SaveChanges();
            }
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public User GetById(Guid id)
        {
            if (context == null)
            {
                context = new AuctionSystemContext();
            }
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool Register(RegisterRequest register)
        {
            if (context.Users.SingleOrDefault(x => x.Username == register.Username) != null)
            {
                return false;
            }
            context.Users.Add(new User()
            { 
                FirstName = register.FirstName,
                SecondName = register.SecondName,
                ThirdName = register.ThirdName,
                Address = register.Address,
                Phone = register.Phone,
                Username = register.Username,
                Email = register.Email,
                Id = Guid.NewGuid(),
                Password = register.Password,
                Role = (int)Roles.Customer
            });
            return context.SaveChanges() > 0;
        }

        public AuthenticateResponse Login(AuthenticateRequest request)
        {
            return Authenticate(request);
        }

        public bool Ban(BanRequest banRequest)
        {
            User currentUser = context.Users.SingleOrDefault(x => x.Id == banRequest.UserId);
            if (currentUser == null)
            {
                return false;
            }
            currentUser.BanDate = banRequest.BanDate;
            currentUser.BanReason = banRequest.BanReason;
            return context.SaveChanges() > 0;
        }

        public bool Unban(Guid userId)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return false;

            user.LockoutAttempts = 0;
            user.BanDate = null;
            user.BanReason = null;
            if (context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
