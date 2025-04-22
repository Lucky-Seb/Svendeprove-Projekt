using Microsoft.AspNetCore.Components;
#if ANDROID || IOS || WINDOWS || MACCATALYST
using Microsoft.Maui.Storage;
#else
using Blazored.LocalStorage;
#endif
using TaekwondoApp.Shared.Services;

namespace TaekwondoApp.Shared.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly AuthStateProvider _authStateProvider;

#if ANDROID || IOS || WINDOWS || MACCATALYST
        // No local storage dependency needed for MAUI
#else
        private readonly ILocalStorageService _localStorage;
#endif

        public AuthenticationService(
            NavigationManager navigationManager,
            AuthStateProvider authStateProvider
#if ANDROID || IOS || WINDOWS || MACCATALYST
            // No DI for SecureStorage, it's static
#else
            , ILocalStorageService localStorage
#endif
        )
        {
            _navigationManager = navigationManager;
            _authStateProvider = authStateProvider;
#if ANDROID || IOS || WINDOWS || MACCATALYST
            // No assignment needed for SecureStorage
#else
            _localStorage = localStorage;
#endif
        }

        public async Task SetTokenAsync(string token)
        {
#if ANDROID || IOS || WINDOWS || MACCATALYST
            await SecureStorage.SetAsync("jwt_token", token);
#else
            await _localStorage.SetItemAsync("jwt_token", token);
#endif
            _authStateProvider.SetAuth(token);
        }

        public async Task<string?> GetTokenAsync()
        {
#if ANDROID || IOS || WINDOWS || MACCATALYST
            return await SecureStorage.GetAsync("jwt_token");
#else
            return await _localStorage.GetItemAsync<string>("jwt_token");
#endif
        }

        public async Task RemoveTokenAsync()
        {
#if ANDROID || IOS || WINDOWS || MACCATALYST
            SecureStorage.Remove("jwt_token");
#else
            await _localStorage.RemoveItemAsync("jwt_token");
#endif
            _authStateProvider.ClearAuth();
        }
    }
}
