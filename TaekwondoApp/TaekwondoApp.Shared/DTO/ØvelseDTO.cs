namespace TaekwondoApp.Shared.DTO
{
    public class ØvelseDTO
    {
        public int ØvelseID { get; set; }
        public string ØvelseNavn { get; set; }
        public string ØvelseBeskrivelse { get; set; }
        public string ØvelseBillede { get; set; }
        public string ØvelseVideo { get; set; }
        public int ØvelseTid { get; set; }
        public string ØvelseSværhed { get; set; }
        public int PensumID { get; set; }
        public int? BrugerID { get; set; }
        public int? KlubID { get; set; }
    }
}
