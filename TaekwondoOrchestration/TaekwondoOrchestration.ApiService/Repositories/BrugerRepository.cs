using Microsoft.EntityFrameworkCore;
using TaekwondoOrchestration.ApiService.Data;
using TaekwondoApp.Shared.Models;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaekwondoOrchestration.ApiService.Repositories
{
    public class BrugerRepository : IBrugerRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public BrugerRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Bruger>> GetAllBrugereAsync()
        {
            return await _context.Brugere.ToListAsync();
        }

        public async Task<Bruger?> GetBrugerByIdAsync(Guid brugerId)
        {
            return await _context.Brugere.FindAsync(brugerId);
        }

        public async Task<List<Bruger>> GetBrugerByRoleAsync(string role)
        {
            return await _context.Brugere
                .Where(b => b.Role == role)
                .ToListAsync();
        }

        public async Task<List<Bruger>> GetBrugerByBælteAsync(string bæltegrad)
        {
            return await _context.Brugere
                .Where(b => b.Bæltegrad == bæltegrad)
                .ToListAsync();
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAsync(Guid klubId)
        {
            return await _context.BrugerKlubber
                .Where(bk => bk.KlubID == klubId)
                .Select(bk => bk.Bruger)
                .Include(b => b.BrugerKlubber)
                .ThenInclude(bk => bk.Klub)
                .ProjectTo<BrugerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<List<BrugerDTO>> GetBrugereByKlubAndBæltegradAsync(Guid klubId, string bæltegrad)
        {
            return await _context.BrugerKlubber
                .Where(bk => bk.KlubID == klubId && bk.Bruger.Bæltegrad == bæltegrad)
                .Select(bk => bk.Bruger)
                .Include(b => b.BrugerKlubber)
                .ThenInclude(bk => bk.Klub)
                .ProjectTo<BrugerDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Bruger?> GetBrugerByBrugernavnAsync(string brugernavn)
        {
            return await _context.Brugere
                .FirstOrDefaultAsync(b => b.Brugernavn == brugernavn);
        }

        public async Task<List<Bruger>> GetBrugerByFornavnEfternavnAsync(string fornavn, string efternavn)
        {
            return await _context.Brugere
                .Where(b => b.Fornavn == fornavn && b.Efternavn == efternavn)
                .ToListAsync();
        }

        public async Task<Bruger> CreateBrugerAsync(Bruger bruger)
        {
            // Hash the password before storing it in the database
            bruger.Brugerkode = BCrypt.Net.BCrypt.HashPassword(bruger.Brugerkode);

            _context.Brugere.Add(bruger);
            await _context.SaveChangesAsync();
            return bruger;
        }

        public async Task<bool> UpdateBrugerAsync(Bruger bruger)
        {
            // Hash the password if it is being updated
            if (!string.IsNullOrEmpty(bruger.Brugerkode))
            {
                bruger.Brugerkode = BCrypt.Net.BCrypt.HashPassword(bruger.Brugerkode);
            }

            _context.Brugere.Update(bruger);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBrugerAsync(Guid brugerId)
        {
            var bruger = await _context.Brugere.FindAsync(brugerId);
            if (bruger == null) return false;

            _context.Brugere.Remove(bruger);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<BrugerDTO?> AuthenticateBrugerAsync(string emailOrBrugernavn, string brugerkode)
        {
            var bruger = await _context.Brugere
                .FirstOrDefaultAsync(b => (b.Email == emailOrBrugernavn || b.Brugernavn == emailOrBrugernavn));

            if (bruger == null) return null;

            // Check if the password matches using bcrypt
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(brugerkode, bruger.Brugerkode);
            if (!passwordMatch) return null;
            Console.WriteLine($"Plain: {brugerkode}");
            Console.WriteLine($"Hashed: {bruger.Brugerkode}");
            Console.WriteLine($"Match: {passwordMatch}");
            // Create JWT Token
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, bruger.Brugernavn),
            new Claim(ClaimTypes.NameIdentifier, bruger.BrugerID.ToString()),
                new Claim(ClaimTypes.Role, bruger.Role) // Add the role claim here
            // Add other claims like roles or email if needed
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HalloIamAPersonOfTheAGreatFamileWHichDOESNOTKNOWHOWTOBESTGETARandomKEyHalloIamAPersonOfTheAGreatFamileWHichDOESNOTKNOWHOWTOBESTGETARandomKEyHalloIamAPersonOfTheAGreatFamileWHichDOESNOTKNOWHOWTOBESTGETARandomKEyHalloIamAPersonOfTheAGreatFamileWHichDOESNOTKNOWHOWTOBESTGETARandomKEy"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Map the user to the DTO and add the token to the DTO
            var userDto = _mapper.Map<BrugerDTO>(bruger);
            userDto.Token = jwt; // Store the JWT in the DTO

            return userDto;
        }
    }
}
