namespace TaekwondoApp.Shared.Models
{
    public class Bruger
    {
        public Guid BrugerID { get; set; }
        public string Email { get; set; }
        public string Brugernavn { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Address { get; set; }
        public string Bæltegrad { get; set; }
        public string Role { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public ICollection<BrugerLogin> Logins { get; set; }
        public string TwoFactorSecret { get; set; } // For TOTP
        public ICollection<BrugerKlub>? BrugerKlubber { get; set; }
        public ICollection<BrugerProgram>? BrugerProgrammer { get; set; }
        public ICollection<BrugerQuiz>? BrugerQuizzer { get; set; }
        public ICollection<BrugerØvelse>? BrugerØvelser { get; set; }
    }
}
