using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.Storage;
using System.Security.Claims;
using TaekwondoApp.Shared.Services;

public class AuthStateProvider : AuthenticationStateProvider
{
    public event Action? OnChange;

    private string? _token;
    private string? _role;

    public string? Token => _token;
    public string? Role => _role;
    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

    public AuthStateProvider()
    {
        _ = InitializeAsync(); // Fire and forget initialization
    }

    private async Task InitializeAsync()
    {
        var token = await SecureStorage.GetAsync("jwt_token");

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
        await SecureStorage.SetAsync("jwt_token", token);
        NotifyStateChanged();
    }

    public async Task ClearAuth()
    {
        _token = null;
        _role = null;
        await SecureStorage.SetAsync("jwt_token", string.Empty);
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

        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
