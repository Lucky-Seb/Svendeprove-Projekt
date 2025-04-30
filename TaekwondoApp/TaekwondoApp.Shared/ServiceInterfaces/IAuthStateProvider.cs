using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IAuthStateProvider
    {
        string? Token { get; }
        string? Role { get; }
        bool IsAuthenticated { get; }

        Task SetAuth(string token);
        Task ClearAuth();
    }
}
