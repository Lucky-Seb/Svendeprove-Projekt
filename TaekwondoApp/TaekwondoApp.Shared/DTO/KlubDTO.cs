namespace TaekwondoApp.Shared.DTO
{
    public class KlubDTO
    {
        public Guid KlubID { get; set; }
        public string KlubNavn { get; set; }

        public List<ProgramPlanDTO> Programmer { get; set; }  // Programs associated with the user
        public List<QuizDTO> Quizzer { get; set; }  // Quizzes associated with the user
        public List<ØvelseDTO> Øvelser { get; set; }  // Exercises associated with the user
        public List<BrugerDTO> Bruger { get; set; }  // Clubs associated with the user

    }
}