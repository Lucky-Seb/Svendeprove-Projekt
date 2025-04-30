using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Shared.Services
{
    public class AuthStateProvider : AuthenticationStateProvider, IAuthStateProvider
    {
        public event Action? OnChange;

        private readonly ITokenStorage _tokenStorage;

        private string? _token;
        private string? _role;

        public string? Token => _token;
        public string? Role => _role;
        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

        public AuthStateProvider(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
            _ = InitializeAsync(); // Fire and forget
        }

        private async Task InitializeAsync()
        {
            var token = await _tokenStorage.GetAsync("jwt_token");

            if (!string.IsNullOrEmpty(token))
            {
                _token = token;
                _role = JwtParser.GetRole(token);
                NotifyStateChanged();
            }
        }

        public async Task SetAuth(string token)
        {
            _token = token;
            _role = JwtParser.GetRole(token);
            await _tokenStorage.SetAsync("jwt_token", token);
            NotifyStateChanged();
        }

        public async Task ClearAuth()
        {
            _token = null;
            _role = null;
            _tokenStorage.Remove("jwt_token");
            NotifyStateChanged();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = IsAuthenticated
                ? new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, "User"),
                    new Claim(ClaimTypes.Role, _role ?? "Guest")
                }, "jwt")
                : new ClaimsIdentity();

            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
