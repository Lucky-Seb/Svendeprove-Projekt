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

        // Here you would typically create a claims identity for the user
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, bruger.Brugernavn),
                // Add other claims here as needed
            }),
            Expires = DateTime.Now.AddHours(1), // Set the expiration time
            Issuer = "YourIssuer", // Can be extracted from config as well
            Audience = "YourAudience", // Can be extracted from config as well
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
