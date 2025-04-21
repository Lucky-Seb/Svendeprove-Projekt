namespace TaekwondoApp.Shared.DTO
{
    public class BrugerKlubDTO
    {
        public Guid BrugerID { get; set; }
        public Guid KlubID { get; set; }
        public string KlubRole { get; set; } // Role of the user in the club
    }
    public class BrugerProgramDTO
    {
        public Guid BrugerID { get; set; }
        public Guid ProgramID { get; set; }
    }
    public class BrugerØvelseDTO
    {
        public Guid BrugerID { get; set; }
        public Guid ØvelseID { get; set; }
    }
    public class BrugerQuizDTO
    {
        public Guid BrugerID { get; set; }
        public Guid QuizID { get; set; }
    }
    public class KlubProgramDTO
    {
        public Guid KlubID { get; set; }
        public Guid ProgramID { get; set; }
    }
    public class KlubQuizDTO
    {
        public Guid KlubID { get; set; }
        public Guid QuizID { get; set; }
    }
    public class KlubØvelseDTO
    {
        public Guid KlubID { get; set; }
        public Guid ØvelseID { get; set; }
    }
}
