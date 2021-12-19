using AuctionSystem.Server.Services;
using AuctionSystem.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionSystem.Server.Utils
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSettings;

        public JWTMiddleware(RequestDelegate next, IOptions<JWTSettings> options)
        {
            _next = next;
            _jwtSettings = options.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(token))
            {
                AttachUserToContext(context, userService, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                { 
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                context.Items["User"] = userService.GetById(userId);
            }
            catch { }
        }
    }
}
