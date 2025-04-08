namespace TaekwondoOrchestration.ApiService.DTO
{
    public class BrugerKlubDTO
    {
        public int BrugerID { get; set; }
        public int KlubID { get; set; }
    }
    public class BrugerProgramDTO
    {
        public int BrugerID { get; set; }
        public int ProgramID { get; set; }
    }
    public class BrugerØvelseDTO
    {
        public int BrugerID { get; set; }
        public int ØvelseID { get; set; }
    }
    public class BrugerQuizDTO
    {
        public int BrugerID { get; set; }
        public int QuizID { get; set; }
    }
    public class KlubProgramDTO
    {
        public int KlubID { get; set; }
        public int ProgramID { get; set; }
    }
    public class KlubQuizDTO
    {
        public int KlubID { get; set; }
        public int QuizID { get; set; }
    }
    public class KlubØvelseDTO
    {
        public int KlubID { get; set; }
        public int ØvelseID { get; set; }
    }
}
