using System.Threading.Tasks;
using Microsoft.JSInterop;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Web.Client.Services
{
    public class WebTokenStorage : ITokenStorage
    {
        private readonly IJSRuntime _jsRuntime;

        public WebTokenStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetAsync(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }

        public async void Remove(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
