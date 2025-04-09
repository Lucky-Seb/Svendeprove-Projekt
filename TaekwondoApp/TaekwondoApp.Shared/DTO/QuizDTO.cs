namespace TaekwondoApp.Shared.DTO
{
    public class QuizDTO
    {
        public int QuizID { get; set; }
        public string QuizNavn { get; set; }
        public string QuizBeskrivelse { get; set; }
        public int PensumID { get; set; }
        public int? BrugerID { get; set; }
        public int? KlubID { get; set; }
        public List<SpørgsmålDTO> Spørgsmål { get; set; } = new List<SpørgsmålDTO>();
    }
}
