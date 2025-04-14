using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.DTO
{
    public class LoginDTO
    {
        public string EmailOrBrugernavn { get; set; } = string.Empty;
        public string Brugerkode { get; set; } = string.Empty;
    }
}
