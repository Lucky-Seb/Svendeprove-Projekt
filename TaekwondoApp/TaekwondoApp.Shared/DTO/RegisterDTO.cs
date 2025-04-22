using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaekwondoApp.Shared.DTO
{
    public class RegisterDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Brugernavn { get; set; } = string.Empty;
        public string Fornavn { get; set; } = string.Empty;
        public string Efternavn { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Bæltegrad { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
