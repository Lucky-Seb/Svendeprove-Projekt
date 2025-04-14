using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.DTO
{
    public class LoginResultDTO
    {
        public bool Requires2FA { get; set; }
        public string Token { get; set; }
    }
}
