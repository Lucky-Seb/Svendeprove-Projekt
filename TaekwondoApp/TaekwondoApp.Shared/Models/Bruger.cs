namespace TaekwondoApp.Shared.Models
{
    public class Bruger
    {
        public Guid BrugerID { get; set; }
        public string Email { get; set; }
        public string Brugernavn { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public string Brugerkode { get; set; }
        public string Address { get; set; }
        public string Bæltegrad { get; set; }
        public string Role { get; set; }

        public ICollection<BrugerKlub>? BrugerKlubber { get; set; }
        public ICollection<BrugerProgram>? BrugerProgrammer { get; set; }
        public ICollection<BrugerQuiz>? BrugerQuizzer { get; set; }
        public ICollection<BrugerØvelse>? BrugerØvelser { get; set; }
        public string? Token { get; set; }  // Nullable since it will only be populated after successful authentication
    }
}
