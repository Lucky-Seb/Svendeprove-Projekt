namespace TaekwondoApp.Shared.DTO
{
    public class TeoriDTO
    {
        public int TeoriID { get; set; }
        public string TeoriNavn { get; set; }
        public string TeoriBeskrivelse { get; set; }
        public string TeoriBillede { get; set; }
        public string TeoriVideo { get; set; }
        public string TeoriLyd { get; set; }
        public int PensumID { get; set; } // Foreign key
    }
}
