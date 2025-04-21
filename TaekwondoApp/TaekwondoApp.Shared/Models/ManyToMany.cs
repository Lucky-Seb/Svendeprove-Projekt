namespace TaekwondoApp.Shared.Models
{
    public class KlubProgram
    {
        public Guid KlubID { get; set; }
        public Guid ProgramID { get; set; }

        public Klub Klub { get; set; }
        public ProgramPlan Plan { get; set; }
    }

    public class BrugerProgram
    {
        public Guid BrugerID { get; set; }
        public Guid ProgramID { get; set; }

        public Bruger Bruger { get; set; }
        public ProgramPlan Plan { get; set; }

    }

    public class BrugerØvelse
    {
        public Guid BrugerID { get; set; }
        public Guid ØvelseID { get; set; }

        public Bruger Bruger { get; set; }
        public Øvelse Øvelse { get; set; }
    }

    public class BrugerQuiz
    {
        public Guid BrugerID { get; set; }
        public Guid QuizID { get; set; }

        public Bruger Bruger { get; set; }
        public Quiz Quiz { get; set; }
    }

    public class KlubQuiz
    {
        public Guid KlubID { get; set; }
        public Guid QuizID { get; set; }

        public Klub Klub { get; set; }
        public Quiz Quiz { get; set; }
    }

    public class BrugerKlub
    {
        public Guid BrugerID { get; set; }
        public Guid KlubID { get; set; }

        public string KlubRole { get; set; } // Role of the user in the club

        public Bruger Bruger { get; set; }
        public Klub Klub { get; set; }
    }

    public class KlubØvelse
    {
        public Guid KlubID { get; set; }
        public Guid ØvelseID { get; set; }

        public Klub Klub { get; set; }
        public Øvelse Øvelse { get; set; }
    }
}
