using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.Services
{
    public interface ISyncService
    {
        Task SyncLocalChangesToServerAsync();
        Task SyncDataFromServerAsync();
    }

}
