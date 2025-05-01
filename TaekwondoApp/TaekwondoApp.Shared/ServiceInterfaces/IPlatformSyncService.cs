using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;

namespace TaekwondoApp.Shared.ServiceInterfaces
{
    public interface IPlatformSyncService
    {
        Task SyncOrdbogAsync();
    }
}
