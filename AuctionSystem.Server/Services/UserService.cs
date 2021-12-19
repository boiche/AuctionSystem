using AuctionSystem.Data.Model;
using AuctionSystem.Server.Models;
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
using System.Security.Cryptography;
using System.Text;

namespace AuctionSystem.Server.Services
{
    public class UserService : BaseService, IUserService
    {
        public void SetContext(AuctionSystemContext context)
        {
            this.context = context;
        }

        private readonly JWTSettings _jwtSettings;

        public UserService(IOptions<JWTSettings> appSettings)
        {
            _jwtSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;
            var token = GenerateJwtToken(user);

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
    }
}
