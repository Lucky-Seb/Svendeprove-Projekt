namespace TaekwondoOrchestration.ApiService.Models
{
    public class Spørgsmål
    {
        public int SpørgsmålID { get; set; }
        public int SpørgsmålRækkefølge { get; set; }
        public int SpørgsmålTid { get; set; }

        //ForeigenKey
        public int? TeoriID { get; set; }
        public int? TeknikID { get; set; }
        public int? ØvelseID { get; set; }
        public int QuizID { get; set; }

        public Quiz? Quiz { get; set; }
        public Teori? Teori { get; set; }
        public Teknik? Teknik { get; set; }
        public Øvelse? Øvelse { get; set; }
    }
}
