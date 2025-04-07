namespace TaekwondoOrchestration.ApiService.Models
{
    public class KlubProgram
    {
        public int KlubID { get; set; }
        public int ProgramID { get; set; }

        public Klub Klub { get; set; }
        public ProgramPlan Plan { get; set; }
    }

    public class BrugerProgram
    {
        public int BrugerID { get; set; }
        public int ProgramID { get; set; }

        public Bruger Bruger { get; set; }
        public ProgramPlan Plan { get; set; }

    }

    public class BrugerØvelse
    {
        public int BrugerID { get; set; }
        public int ØvelseID { get; set; }

        public Bruger Bruger { get; set; }
        public Øvelse Øvelse { get; set; }
    }

    public class BrugerQuiz
    {
        public int BrugerID { get; set; }
        public int QuizID { get; set; }

        public Bruger Bruger { get; set; }
        public Quiz Quiz { get; set; }
    }

    public class KlubQuiz
    {
        public int KlubID { get; set; }
        public int QuizID { get; set; }

        public Klub Klub { get; set; }
        public Quiz Quiz { get; set; }
    }

    public class BrugerKlub
    {
        public int BrugerID { get; set; }
        public int KlubID { get; set; }

        public Bruger Bruger { get; set; }
        public Klub Klub { get; set; }
    }

    public class KlubØvelse
    {
        public int KlubID { get; set; }
        public int ØvelseID { get; set; }

        public Klub Klub { get; set; }
        public Øvelse Øvelse { get; set; }
    }
}
