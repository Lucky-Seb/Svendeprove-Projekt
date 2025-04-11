namespace TaekwondoApp.Shared.Models
{
    public class Quiz
    {
        public Guid QuizID { get; set; }
        public string QuizNavn { get; set; }
        public string QuizBeskrivelse { get; set; }

        //ForeigenKey
        public Guid PensumID { get; set; }
        public Pensum Pensum { get; set; }

        public ICollection<BrugerQuiz>? BrugerQuizzer { get; set; }
        public ICollection<KlubQuiz>? KlubQuizzer { get; set; }
        public ICollection<Spørgsmål> Spørgsmåls { get; set; }

    }
}
