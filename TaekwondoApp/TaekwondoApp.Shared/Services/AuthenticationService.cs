﻿using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Storage; // <-- Make sure this is included
using System.Net.Http.Headers;
using TaekwondoApp.Shared.Services;

namespace TaekwondoApp.Shared.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;
        private readonly AuthStateProvider _authStateProvider;

        public AuthenticationService(NavigationManager navigationManager, AuthStateProvider authStateProvider)
        {
            _navigationManager = navigationManager;
            _authStateProvider = authStateProvider;
        }

        public async Task SetTokenAsync(string token)
        {
            if (!OperatingSystem.IsBrowser())
            {
                await SecureStorage.SetAsync("jwt_token", token);
            }

            _authStateProvider.SetAuth(token);
        }

        public async Task<string?> GetTokenAsync()
        {
            if (OperatingSystem.IsBrowser())
            {
                return null;
            }
            return await SecureStorage.GetAsync("jwt_token");
        }

        public async Task RemoveTokenAsync()
        {
            if (!OperatingSystem.IsBrowser())
            {
                SecureStorage.Remove("jwt_token");
            }

            _authStateProvider.ClearAuth();
        }
        public async Task<Guid> GetUserIdFromTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty", nameof(token));
            }

            try
            {
                // Extract the userId from the JWT token
                return JwtParser.GetUserIdFromJwt(token);
            }
            catch (Exception ex)
            {
                // Handle error if userId cannot be extracted
                Console.WriteLine($"Error extracting UserId from token: {ex.Message}");
                throw;
            }
        }
    }
}
