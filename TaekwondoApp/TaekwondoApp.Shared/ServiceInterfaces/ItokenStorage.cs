using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface ITokenStorage
    {
        Task SetAsync(string key, string value);
        Task<string?> GetAsync(string key);
        void Remove(string key);
    }

}
