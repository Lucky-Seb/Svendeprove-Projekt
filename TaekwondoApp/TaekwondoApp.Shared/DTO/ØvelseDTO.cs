namespace TaekwondoApp.Shared.DTO
{
    public class ØvelseDTO : SyncableEntityDTO
    {
        public Guid ØvelseID { get; set; }
        public string ØvelseNavn { get; set; }
        public string ØvelseBeskrivelse { get; set; }
        public string ØvelseBillede { get; set; }
        public string ØvelseVideo { get; set; }
        public int ØvelseTid { get; set; }
        public string ØvelseSværhed { get; set; }
        public Guid PensumID { get; set; }
        public Guid? BrugerID { get; set; }
        public Guid? KlubID { get; set; }
    }
}
