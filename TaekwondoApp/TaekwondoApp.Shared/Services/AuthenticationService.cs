using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Storage; // <-- Make sure this is included
using System.Net.Http.Headers;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Shared.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IAuthStateProvider _authStateProvider;  // Changed from AuthStateProvider to IAuthStateProvider
        private readonly ITokenStorage _tokenStorage;

        public AuthenticationService(
            NavigationManager navigationManager,
            IAuthStateProvider authStateProvider,  // Changed from AuthStateProvider to IAuthStateProvider
            ITokenStorage tokenStorage)
        {
            _navigationManager = navigationManager;
            _authStateProvider = authStateProvider;
            _tokenStorage = tokenStorage;
        }

        public async Task SetTokenAsync(string token)
        {
            await _tokenStorage.SetAsync("jwt_token", token);
            _authStateProvider.SetAuth(token);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _tokenStorage.GetAsync("jwt_token");
        }

        public async Task RemoveTokenAsync()
        {
            _tokenStorage.Remove("jwt_token");
            _authStateProvider.ClearAuth();
        }
    }
}
