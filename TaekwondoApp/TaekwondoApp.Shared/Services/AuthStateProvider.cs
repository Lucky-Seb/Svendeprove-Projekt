using Microsoft.AspNetCore.Components.Authorization;
#if ANDROID || IOS || WINDOWS || MACCATALYST
using Microsoft.Maui.Storage;
#else
using Blazored.LocalStorage;
#endif
using System.Security.Claims;
using TaekwondoApp.Shared.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    public event Action? OnChange;

    private readonly ILocalStorageService _localStorage;

    private string? _token;
    private string? _role;

    public string? Token => _token;
    public string? Role => _role;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

    public AuthStateProvider(
#if ANDROID || IOS || WINDOWS || MACCATALYST
        // No localStorage dependency needed for MAUI (use SecureStorage)
#else
        ILocalStorageService localStorage
#endif
    )
    {
#if ANDROID || IOS || WINDOWS || MACCATALYST
        // No localStorage needed for MAUI, it's handled with SecureStorage
#else
        _localStorage = localStorage;
#endif
        _ = InitializeAsync(); // Fire and forget initialization
    }

    private async Task InitializeAsync()
    {
        string? token = null;

#if ANDROID || IOS || WINDOWS || MACCATALYST
        token = await SecureStorage.GetAsync("jwt_token");
#else
        token = await _localStorage.GetItemAsync<string>("jwt_token");
#endif

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

#if ANDROID || IOS || WINDOWS || MACCATALYST
        await SecureStorage.SetAsync("jwt_token", token);
#else
        await _localStorage.SetItemAsync("jwt_token", token);
#endif

        NotifyStateChanged();
    }

    public async Task ClearAuth()
    {
        _token = null;
        _role = null;

#if ANDROID || IOS || WINDOWS || MACCATALYST
        SecureStorage.Remove("jwt_token");
#else
        await _localStorage.RemoveItemAsync("jwt_token");
#endif

        NotifyStateChanged();
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = IsAuthenticated
            ? new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, _role ?? "Guest")
            }, "jwt")
            : new ClaimsIdentity();

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
