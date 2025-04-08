namespace TaekwondoOrchestration.ApiService.DTO
{
    public class TræningDTO
    {
        public int TræningID { get; set; }
        public int TræningRækkefølge { get; set; }
        public int Tid { get; set; }
        public int ProgramID { get; set; }
        public int? QuizID { get; set; }
        public int? TeoriID { get; set; }
        public int? TeknikID { get; set; }
        public int? ØvelseID { get; set; }
        public int? PensumID { get; set; }

        // Change from List<> to singular objects for one-to-one relationships
        public QuizDTO? Quiz { get; set; }
        public TeoriDTO? Teori { get; set; }
        public TeknikDTO? Teknik { get; set; }
        public ØvelseDTO? Øvelse { get; set; }
    }
}
