using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.Services
{
    public class AuthStateProvider
    {
        public event Action? OnChange;

        private string? _token;
        public string? Token => _token;

        private string? _role;
        public string? Role => _role;

        public bool IsAuthenticated => !string.IsNullOrEmpty(_token);

        public void SetAuth(string token)
        {
            _token = token;
            _role = JwtParser.GetRole(token);
            NotifyStateChanged();
        }

        public void ClearAuth()
        {
            _token = null;
            _role = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
