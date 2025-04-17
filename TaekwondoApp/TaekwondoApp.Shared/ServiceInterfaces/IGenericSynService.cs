using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IGenericSyncService<T, TDto>
        where T : SyncableEntity, new()
        where TDto : class
    {
        Task SyncDataAsync(string apiEndpoint);
    }
}
