using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.DTO
{
    public class BrugerUpdateDTO : SyncableEntityDTO
    {
        public string? Email { get; set; }
        public string? Brugernavn { get; set; }
        public string? Fornavn { get; set; }
        public string? Efternavn { get; set; }
        public string? Address { get; set; }
        public string? Bæltegrad { get; set; }
        public string? Role { get; set; }
        public string? Brugerkode { get; set; }  // Optional - for password updates only
    }
}
