using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IOrdbogSyncService
    {
        Task SyncDataAsync();
    }

}
