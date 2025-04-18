using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly string _secretKey;

        public JwtHelper(string secretKey)
        {
            _secretKey = secretKey;
        }

        public string GenerateToken(Bruger bruger)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, bruger.Brugernavn),
                new Claim(ClaimTypes.NameIdentifier, bruger.BrugerID.ToString()),
                new Claim(ClaimTypes.Role, bruger.Role)
            };

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
