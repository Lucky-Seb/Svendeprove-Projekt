namespace TaekwondoApp.Shared.DTO
{
    public class SpørgsmålDTO : SyncableEntityDTO
    {
        public Guid SpørgsmålID { get; set; }
        public int SpørgsmålRækkefølge { get; set; }
        public int SpørgsmålTid { get; set; }
        public Guid? TeoriID { get; set; }
        public Guid? TeknikID { get; set; }
        public Guid? ØvelseID { get; set; }
        public TeknikDTO Teknik { get; set; }
        public TeoriDTO Teori { get; set; }
        public ØvelseDTO Øvelse { get; set; }
        public Guid QuizID { get; set; }
    }
}
