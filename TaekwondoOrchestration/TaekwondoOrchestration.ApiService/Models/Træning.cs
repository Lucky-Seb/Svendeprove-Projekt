namespace TaekwondoOrchestration.ApiService.Models
{
    public class Træning
    {
        public Guid TræningID { get; set; }
        public int TræningRækkefølge { get; set; }
        public int Tid { get; set; }

        // Foreign keys (single items)
        public Guid ProgramID { get; set; }
        public Guid? QuizID { get; set; }
        public Guid? TeoriID { get; set; }
        public Guid? TeknikID { get; set; }
        public Guid? ØvelseID { get; set; }
        public Guid? PensumID { get; set; }

        // Navigation properties (single items)
        public Quiz? Quiz { get; set; }
        public Teori? Teori { get; set; }
        public Teknik? Teknik { get; set; }
        public Øvelse? Øvelse { get; set; }
        public Pensum? Pensum { get; set; }

        public ProgramPlan ProgramPlan { get; set; }
    }
}
