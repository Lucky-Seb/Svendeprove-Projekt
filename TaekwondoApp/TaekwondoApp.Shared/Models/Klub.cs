namespace TaekwondoApp.Shared.Models
{
    public class Klub
    {
        public Guid KlubID { get; set; }
        public string KlubNavn { get; set; }

        public ICollection<KlubProgram>? KlubProgrammer { get; set; }
        public ICollection<KlubQuiz>? KlubQuizzer { get; set; }
        public ICollection<KlubØvelse>? KlubØvelser { get; set; }
        public ICollection<BrugerKlub>? BrugerKlubber { get; set; }
    }
}
