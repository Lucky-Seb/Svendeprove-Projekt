namespace TaekwondoApp.Shared.DTO
{
    public class TræningDTO : SyncableEntityDTO
    {
        public Guid TræningID { get; set; }
        public int TræningRækkefølge { get; set; }
        public int Tid { get; set; }
        public Guid ProgramID { get; set; }
        public Guid? QuizID { get; set; }
        public Guid? TeoriID { get; set; }
        public Guid? TeknikID { get; set; }
        public Guid? ØvelseID { get; set; }
        public Guid? PensumID { get; set; }

        // Change from List<> to singular objects for one-to-one relationships
        public QuizDTO? Quiz { get; set; }
        public TeoriDTO? Teori { get; set; }
        public TeknikDTO? Teknik { get; set; }
        public ØvelseDTO? Øvelse { get; set; }
    }
}
