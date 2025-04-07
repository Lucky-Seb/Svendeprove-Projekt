namespace TaekwondoOrchestration.ApiService.Models
{
    public class Øvelse
    {
        public int ØvelseID { get; set; }
        public string ØvelseNavn { get; set; }
        public string ØvelseBeskrivelse { get; set; }
        public string ØvelseBillede { get; set; }
        public string ØvelseVideo { get; set; }
        public int ØvelseTid { get; set; }
        public string ØvelseSværhed { get; set; }

        public int PensumID { get; set; }  // Foreign Key

        public ICollection<BrugerØvelse>? BrugerØvelses { get; set; }
        public ICollection<KlubØvelse>? KlubØvelses { get; set; }
    }
}
