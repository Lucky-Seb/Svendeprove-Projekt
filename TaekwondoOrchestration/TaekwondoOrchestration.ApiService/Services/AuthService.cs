using TaekwondoApp.Shared.Models;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using TaekwondoOrchestration.ApiService.RepositorieInterfaces;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.Helpers;
using Microsoft.AspNetCore.Mvc;
using static TaekwondoApp.Shared.Pages.Registration;

namespace TaekwondoOrchestration.ApiService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBrugerRepository _brugerRepository;
        private readonly ITokenService _tokenService;
        private readonly ITotpService _totpService;

        public AuthService(IBrugerRepository brugerRepository, ITokenService tokenService, ITotpService totpService)
        {
            _brugerRepository = brugerRepository;
            _tokenService = tokenService;
            _totpService = totpService;
        }

        public async Task<LoginResultDTO> LoginAsync(string brugername, string password)
        {
            var bruger = await _brugerRepository.GetBrugerByBrugernavnAsync(brugername);
            if (bruger == null)
                return null;

            var localLogin = bruger.Logins.FirstOrDefault(l => l.Provider == "local");
            if (localLogin == null || !PasswordHelper.VerifyPassword(password, localLogin.PasswordHash))
                return null;

            if (bruger.TwoFactorEnabled)
                return new LoginResultDTO { Requires2FA = true };

            var token = _tokenService.GenerateJwtToken(bruger);
            return new LoginResultDTO { Token = token };
        }


        public async Task<string> Verify2FACodeAsync(string email, string code)
        {
            var bruger = await _brugerRepository.GetBrugerByEmailAsync(email);
            if (bruger == null)
                return null;

            // Verify the TOTP code using the ITotpService
            if (!_totpService.ValidateTotp(bruger.TwoFactorSecret, code))
                return null;

            return _tokenService.GenerateJwtToken(bruger);  // Return the JWT token after successful 2FA verification
        }


        public async Task<string> OAuthCallbackAsync(string provider, string email)
        {
            var userLogin = await _brugerRepository.GetBrugerLoginAsync(provider, email);
            Bruger bruger;

            if (userLogin != null)
            {
                bruger = userLogin.Bruger;
            }
            else
            {
                // Create new bruger and login
                bruger = new Bruger
                {
                    Email = email,
                    Brugernavn = email,
                    TwoFactorEnabled = false,
                    Logins = new List<BrugerLogin>
            {
                new BrugerLogin
                {
                    Provider = provider,
                    ProviderKey = email
                }
            }
                };
                await _brugerRepository.CreateBrugerAsync(bruger);
            }

            return _tokenService.GenerateJwtToken(bruger);
        }


        public async Task<string> Generate2FAQRCodeAsync(string brugername)
        {
            var bruger = await _brugerRepository.GetBrugerByEmailAsync(brugername);
            if (bruger == null)
                return null;

            bruger.TwoFactorSecret = _totpService.GenerateSecret();
            await _brugerRepository.UpdateBrugerAsync(bruger);

            var qrCodeUrl = _totpService.GenerateQrCodeUrl(bruger.TwoFactorSecret, brugername);
            return qrCodeUrl;
        }

        public async Task<bool> Verify2FASetupAsync(string email, string code)
        {
            var bruger = await _brugerRepository.GetBrugerByEmailAsync(email);
            if (bruger == null || !_totpService.ValidateTotp(bruger.TwoFactorSecret, code))
                return false;

            bruger.TwoFactorEnabled = true;
            await _brugerRepository.UpdateBrugerAsync(bruger);
            return true;
        }
        public async Task<Bruger> RegisterLocalAsync(string brugernavn, string password)
        {
            var existing = await _brugerRepository.GetBrugerByEmailAsync(brugernavn);
            if (existing != null) return null;

            var bruger = new Bruger
            {
                Brugernavn = brugernavn,
                Email = brugernavn,
                TwoFactorEnabled = false,
                Logins = new List<BrugerLogin>
        {
            new BrugerLogin
            {
                Provider = "local",
                ProviderKey = brugernavn,
                PasswordHash = PasswordHelper.HashPassword(password)
            }
        }
            };

            await _brugerRepository.CreateBrugerAsync(bruger);
            return bruger;
        }
        public async Task<IActionResult> RegisterAsync(RegisterModel registerModel)
        {
            // Check if the user already exists by email or username
            var existingUserByEmail = await _brugerRepository.GetBrugerByEmailAsync(registerModel.Email);
            if (existingUserByEmail != null)
            {
                return new BadRequestObjectResult("User with this email already exists.");
            }

            var existingUserByUsername = await _brugerRepository.GetBrugerByBrugernavnAsync(registerModel.Username);
            if (existingUserByUsername != null)
            {
                return new BadRequestObjectResult("User with this username already exists.");
            }

            // Create a new user and login
            var newUser = new Bruger
            {
                BrugerID = Guid.NewGuid(),
                Email = registerModel.Email,
                Brugernavn = registerModel.Username,
                Fornavn = registerModel.Fornavn,
                Efternavn = registerModel.Efternavn,
                Address = registerModel.Address,
                Role = "User"  // default role, you can adjust as needed
            };

            // Hash the password before storing
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);

            var newLogin = new BrugerLogin
            {
                LoginId = Guid.NewGuid(),
                Provider = "local",  // Local login
                ProviderKey = registerModel.Email,
                PasswordHash = passwordHash,
                BrugerID = newUser.BrugerID
            };

            // Save the user and login info
            await _brugerRepository.CreateBrugerAsync(newUser, newLogin);

            return new OkObjectResult("User registered successfully.");
        }
    }
}
