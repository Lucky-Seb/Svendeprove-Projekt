using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Web.Services
{
    public class ServerTokenStorage : ITokenStorage
    {
        private readonly Dictionary<string, string> _memory = new();

        public Task SetAsync(string key, string value)
        {
            _memory[key] = value;
            return Task.CompletedTask;
        }

        public Task<string?> GetAsync(string key)
        {
            _memory.TryGetValue(key, out var value);
            return Task.FromResult<string?>(value);
        }

        public void Remove(string key)
        {
            _memory.Remove(key);
        }
    }

}
