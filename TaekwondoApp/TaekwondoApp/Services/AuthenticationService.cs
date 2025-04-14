using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Services;

namespace TaekwondoApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //private readonly ILocalStorageService _localStorageService; // For WebAssembly
        private readonly ISecureStorage _secureStorageService; // For .NET MAUI
        private readonly NavigationManager _navigationManager;

        public AuthenticationService(/*ILocalStorageService localStorageService,*/
                                     ISecureStorage secureStorageService,
                                     NavigationManager navigationManager)
        {
            //_localStorageService = localStorageService;
            _secureStorageService = secureStorageService;
            _navigationManager = navigationManager;
        }

        public async Task SetTokenAsync(string token)
        {
            if (OperatingSystem.IsBrowser())
            {
                //await _localStorageService.SetItemAsync("jwt_token", token);
            }
            else
            {
                await _secureStorageService.SetAsync("jwt_token", token);
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            if (OperatingSystem.IsBrowser())
            {
                //return await _localStorageService.GetItemAsync<string>("jwt_token");
                return null; // Placeholder for local storage retrieval
            }
            else
            {
                return await _secureStorageService.GetAsync("jwt_token");
            }
        }

        public async Task RemoveTokenAsync()
        {
            if (OperatingSystem.IsBrowser())
            {
                //await _localStorageService.RemoveItemAsync("jwt_token");
            }
            else
            {
                //await _secureStorageService.RemoveAsync("jwt_token");
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
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
