namespace TaekwondoOrchestration.ApiService.DTO
{
    public class TeknikDTO
    {
        public int TeknikID { get; set; }
        public string TeknikNavn { get; set; }
        public string TeknikBeskrivelse { get; set; }
        public string TeknikBillede { get; set; }
        public string TeknikVideo { get; set; }
        public string TeknikLyd { get; set; }
        public int PensumID { get; set; }
    }
}
