using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Maui.Storage;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Services;

public class AuthStateProvider
{
    // Event to notify other components that authentication state has changed
    public event Action? OnChange;

    private string? _token;
    private string? _role;

    public string? Token => _token;
    public string? Role => _role;

    public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

    // Method to set the token and role, and notify listeners of state change
    public async Task SetAuth(string token)
    {
        _token = token;
        _role = JwtParser.GetRole(token);

        // Store the token securely
        await SecureStorage.SetAsync("jwt_token", token);

        // Notify subscribers that the authentication state has changed
        NotifyStateChanged();
    }

    // Method to clear authentication data, overwrite the token, and notify listeners
    // Method to clear authentication data, overwrite the token, and notify listeners
    // Method to clear authentication data, overwrite the token, and notify listeners
    public async Task ClearAuth()
    {
        _token = null;
        _role = null;

        // Attempt to overwrite the token with an empty string (or null, depending on platform)
        await SecureStorage.SetAsync("jwt_token", string.Empty);

        // Notify subscribers that the authentication state has changed
        NotifyStateChanged();
    }

    // Method to get the current authentication state (i.e., whether the user is authenticated or not)
    public async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await SecureStorage.GetAsync("jwt_token");

        if (!string.IsNullOrEmpty(token))
        {
            _token = token;
            _role = JwtParser.GetRole(token); // Extract role from token
        }

        var identity = IsAuthenticated
            ? new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, _role ?? "Guest")
            }, "jwt")
            : new ClaimsIdentity();

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    // Notify all subscribers that the state has changed
    private void NotifyStateChanged() => OnChange?.Invoke();
}
