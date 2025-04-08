namespace TaekwondoOrchestration.ApiService.Models
{
    public class Teori
    {
        public int TeoriID { get; set; }
        public string TeoriNavn { get; set; }
        public string TeoriBeskrivelse { get; set; }
        public string TeoriBillede { get; set; }
        public string TeoriVideo { get; set; }
        public string TeoriLyd { get; set; }

        public int PensumID { get; set; }  // Foreign Key
        public Pensum Pensum { get; set; } // Navigation Property
    }
}
