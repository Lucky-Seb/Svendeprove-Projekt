namespace TaekwondoApp.Shared.DTO
{
    public class SpørgsmålDTO
    {
        public int SpørgsmålID { get; set; }
        public int SpørgsmålRækkefølge { get; set; }
        public int SpørgsmålTid { get; set; }
        public int? TeoriID { get; set; }
        public int? TeknikID { get; set; }
        public int? ØvelseID { get; set; }
        public TeknikDTO Teknik { get; set; }
        public TeoriDTO Teori { get; set; }
        public ØvelseDTO Øvelse { get; set; }
        public int QuizID { get; set; }
    }
}
