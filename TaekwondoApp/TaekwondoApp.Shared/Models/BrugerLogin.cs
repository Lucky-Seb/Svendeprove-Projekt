using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.Models
{
    public class BrugerLogin
    {
        public Guid LoginId { get; set; }
        public string Provider { get; set; } // "local", "google", "microsoft"
        public string ProviderKey { get; set; } // Email or Google/MS ID
        public string PasswordHash { get; set; } // Only for local logins

        //Forigen Keys
        public Guid BrugerID { get; set; }
        public Bruger Bruger { get; set; }
    }
}
