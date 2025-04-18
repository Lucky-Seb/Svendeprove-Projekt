namespace TaekwondoApp.Shared.DTO
{
    public class QuizDTO : SyncableEntityDTO
    {
        public Guid QuizID { get; set; }
        public string QuizNavn { get; set; }
        public string QuizBeskrivelse { get; set; }
        public Guid PensumID { get; set; }
        public Guid? BrugerID { get; set; }
        public Guid? KlubID { get; set; }
        public List<SpørgsmålDTO> Spørgsmål { get; set; } = new List<SpørgsmålDTO>();
    }
}
