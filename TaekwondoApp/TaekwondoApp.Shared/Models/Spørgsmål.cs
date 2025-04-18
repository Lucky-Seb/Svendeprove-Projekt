namespace TaekwondoApp.Shared.Models
{
    public class Spørgsmål : SyncableEntity
    {
        public Guid SpørgsmålID { get; set; }
        public int SpørgsmålRækkefølge { get; set; }
        public int SpørgsmålTid { get; set; }

        //ForeigenKey
        public Guid? TeoriID { get; set; }
        public Guid? TeknikID { get; set; }
        public Guid? ØvelseID { get; set; }
        public Guid QuizID { get; set; }

        public Quiz? Quiz { get; set; }
        public Teori? Teori { get; set; }
        public Teknik? Teknik { get; set; }
        public Øvelse? Øvelse { get; set; }
    }
}
