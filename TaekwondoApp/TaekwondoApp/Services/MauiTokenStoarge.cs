using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.ServiceInterfaces;

namespace TaekwondoApp.Services
{
    public class MauiTokenStorage : ITokenStorage
    {
        public Task SetAsync(string key, string value) => SecureStorage.SetAsync(key, value);

        public Task<string?> GetAsync(string key) => SecureStorage.GetAsync(key);

        public void Remove(string key) => SecureStorage.Remove(key);
    }
}
