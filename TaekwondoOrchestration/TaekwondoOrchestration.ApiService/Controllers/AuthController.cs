using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using TaekwondoApp.Shared.DTO;
using TaekwondoOrchestration.ApiService.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.EntityFrameworkCore;

namespace TaekwondoOrchestration.ApiService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // 1. Login with Username/Password
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var result = await _authService.LoginAsync(request.Brugernavn, request.Password);
            if (result.Requires2FA)
                return Ok(new { requires2fa = true });

            return Ok(new LoginResponseDTO { Token = result.Token });
        }

        // 2. Verify 2FA Code
        [HttpPost("verify-2fa")]
        public async Task<IActionResult> VerifyTwoFactor([FromBody] Verify2FADTO request)
        {
            var token = await _authService.Verify2FACodeAsync(request.Email, request.Code);
            if (token != null)
                return Ok(new { token });

            return Unauthorized("Invalid 2FA code.");
        }

        // 3. Google OAuth Login
        [HttpGet("externallogin-google")]
        public IActionResult ExternalLoginGoogle()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("OAuthCallback")
            };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        // 4. Microsoft OAuth Login
        [HttpGet("externallogin-microsoft")]
        public IActionResult ExternalLoginMicrosoft()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action("OAuthCallback")
            };
            return Challenge(props, MicrosoftAccountDefaults.AuthenticationScheme);
        }

        // 5. OAuth Callback (handles both Google and Microsoft)
        [HttpGet("oauth-callback")]
        public async Task<IActionResult> OAuthCallback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return Unauthorized();

            var provider = result.Properties.Items["scheme"];
            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;

            var token = await _authService.OAuthCallbackAsync(provider, email);
            return Ok(new { token });
        }

        // 6. Generate TOTP QR Code for 2FA Setup
        [HttpGet("generate-2fa-qr")]
        public async Task<IActionResult> Generate2FAQRCode()
        {
            var qrCodeUrl = await _authService.Generate2FAQRCodeAsync(User.Identity.Name);
            return Ok(new { qrCodeUrl });
        }

        // 7. Verify 2FA Setup (initial validation)
        [HttpPost("verify-2fa-setup")]
        public async Task<IActionResult> Verify2FASetup([FromBody] Verify2FADTO request)
        {
            var isValid = await _authService.Verify2FASetupAsync(request.Email, request.Code);
            if (isValid)
                return Ok(new { success = true });

            return Unauthorized("Invalid 2FA code.");
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            var user = await _authService.RegisterLocalAsync(dto.Brugernavn, dto.Password);
            return Ok(new { success = user != null });
        }

    }
}