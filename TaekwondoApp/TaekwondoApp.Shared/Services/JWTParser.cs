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

            // Debugging: Print all claims to verify the role claim type
            foreach (var claim in claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
            }

            // Look for both 'role' and 'ClaimTypes.Role' (in case of case sensitivity or different formats)
            return claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role, StringComparison.OrdinalIgnoreCase) || c.Type.Equals("role", StringComparison.OrdinalIgnoreCase))?.Value;
        }
        public static string? GetBruger(string jwt)
        {
            var claims = ParseClaimsFromJwt(jwt);
            return claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
