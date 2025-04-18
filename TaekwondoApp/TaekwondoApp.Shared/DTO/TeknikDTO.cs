namespace TaekwondoApp.Shared.DTO
{
    public class TeknikDTO : SyncableEntityDTO
    {
        public Guid TeknikID { get; set; }
        public string TeknikNavn { get; set; }
        public string TeknikBeskrivelse { get; set; }
        public string TeknikBillede { get; set; }
        public string TeknikVideo { get; set; }
        public string TeknikLyd { get; set; }
        public Guid PensumID { get; set; }
    }
}
