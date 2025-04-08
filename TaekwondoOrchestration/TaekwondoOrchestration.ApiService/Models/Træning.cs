namespace TaekwondoOrchestration.ApiService.Models
{
    public class Træning
    {
        public int TræningID { get; set; }
        public int TræningRækkefølge { get; set; }
        public int Tid { get; set; }
        public int ProgramID { get; set; }

        // Foreign keys (single items)
        public int? QuizID { get; set; }
        public int? TeoriID { get; set; }
        public int? TeknikID { get; set; }
        public int? ØvelseID { get; set; }
        public int? PensumID { get; set; }

        // Navigation properties (single items)
        public Quiz? Quiz { get; set; }
        public Teori? Teori { get; set; }
        public Teknik? Teknik { get; set; }
        public Øvelse? Øvelse { get; set; }
        public Pensum? Pensum { get; set; }

        public ProgramPlan ProgramPlan { get; set; }
    }
}
