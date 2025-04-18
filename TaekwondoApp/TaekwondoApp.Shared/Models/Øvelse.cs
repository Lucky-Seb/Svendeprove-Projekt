namespace TaekwondoApp.Shared.Models
{
    public class Øvelse : SyncableEntity
    {
        public Guid ØvelseID { get; set; }
        public string ØvelseNavn { get; set; }
        public string ØvelseBeskrivelse { get; set; }
        public string ØvelseBillede { get; set; }
        public string ØvelseVideo { get; set; }
        public int ØvelseTid { get; set; }
        public string ØvelseSværhed { get; set; }

        public Guid PensumID { get; set; }  // Foreign Key
        public Pensum Pensum { get; set; }

        public ICollection<BrugerØvelse>? BrugerØvelses { get; set; }
        public ICollection<KlubØvelse>? KlubØvelses { get; set; }
    }
}
