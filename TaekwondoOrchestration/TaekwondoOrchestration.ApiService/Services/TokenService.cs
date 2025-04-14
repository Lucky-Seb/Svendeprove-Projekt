using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.Services
{


    namespace YourApp.Services
    {
        public class TokenService : ITokenService
        {
            private readonly IConfiguration _configuration;

            public TokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerateJwtToken(Bruger bruger)
            {
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, bruger.BrugerID.ToString()),
                new Claim(ClaimTypes.Name, bruger.Brugernavn ?? string.Empty),
                new Claim(ClaimTypes.Email, bruger.Email ?? string.Empty)
                // Add roles, permissions etc. here if needed
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(6),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = issuer,
                    Audience = audience
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
        }
    }

}
