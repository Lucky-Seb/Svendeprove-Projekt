using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Storage; // <-- Make sure this is included
using System.Net.Http.Headers;
using TaekwondoApp.Shared.Services;

namespace TaekwondoApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly NavigationManager _navigationManager;

        public AuthenticationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public async Task SetTokenAsync(string token)
        {
            if (OperatingSystem.IsBrowser())
            {
                // Add local storage logic if needed for Blazor WASM
            }
            else
            {
                await SecureStorage.SetAsync("jwt_token", token);
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            if (OperatingSystem.IsBrowser())
            {
                return null; // Add local storage logic if needed
            }
            else
            {
                return await SecureStorage.GetAsync("jwt_token");
            }
        }

        public async Task RemoveTokenAsync()
        {
            if (!OperatingSystem.IsBrowser())
            {
                SecureStorage.Remove("jwt_token");
            }
        }

        public class JwtAuthMessageHandler : DelegatingHandler
        {
            private readonly IAuthenticationService _authenticationService;

            public JwtAuthMessageHandler(IAuthenticationService authenticationService)
            {
                _authenticationService = authenticationService;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var token = await _authenticationService.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
