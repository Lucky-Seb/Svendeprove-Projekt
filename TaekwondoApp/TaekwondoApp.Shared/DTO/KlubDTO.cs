namespace TaekwondoApp.Shared.DTO
{
    public class KlubDTO
    {
        public Guid KlubID { get; set; }
        public string KlubNavn { get; set; }

        // Using ICollection<T> for a modifiable collection (flexible)
        public ICollection<BrugerDTO> Bruger { get; set; } = new List<BrugerDTO>();
        public ICollection<ProgramPlanDTO> Programmer { get; set; } = new List<ProgramPlanDTO>();
        public ICollection<QuizDTO> Quizzer { get; set; } = new List<QuizDTO>();
        public ICollection<ØvelseDTO> Øvelser { get; set; } = new List<ØvelseDTO>();
    }
}
