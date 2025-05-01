using System;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IAuthStateProvider
    {
        string? Token { get; }
        string? Role { get; }
        bool IsAuthenticated { get; }
        event Action? OnChange;  // Add the event definition

        Task SetAuth(string token);
        Task ClearAuth();
    }
}
