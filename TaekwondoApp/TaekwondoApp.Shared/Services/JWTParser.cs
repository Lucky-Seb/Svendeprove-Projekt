using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace TaekwondoApp.Shared.Services
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            return token.Claims;
        }

        public static string? GetRole(string jwt)
        {
            var claims = ParseClaimsFromJwt(jwt);
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        }
    }
}
