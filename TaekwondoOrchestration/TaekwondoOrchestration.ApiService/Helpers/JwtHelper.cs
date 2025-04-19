using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.Helpers;

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

        // Create the signing credentials using the secret key
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create the claims identity
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, bruger.Brugernavn),  // Store the username as a claim
        new Claim("BrugerID", bruger.BrugerID.ToString())         // Add the brugerID as a claim (BrugerID)
    };

        // Add the role as a claim if it exists
        if (!string.IsNullOrEmpty(bruger.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, bruger.Role));  // Role as a claim
        }

        // Create the token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),  // Add the claims (including UserId and role)
            Expires = DateTime.Now.AddHours(1),    // Set the expiration time
            Issuer = "YourIssuer",                 // Can be extracted from config
            Audience = "YourAudience",             // Can be extracted from config
            SigningCredentials = credentials
        };

        // Create the token and return the JWT token string
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
