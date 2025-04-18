namespace TaekwondoApp.Shared.DTO
{
    public class PensumDTO : SyncableEntityDTO
    {
        public Guid PensumID { get; set; }
        public string PensumGrad { get; set; }

        // List of TeknikDTO because one Pensum can have multiple Tekniks
        public List<TeknikDTO> Teknik { get; set; } = new List<TeknikDTO>();

        // List of TeoriDTO because one Pensum can have multiple Teoris
        public List<TeoriDTO> Teori { get; set; } = new List<TeoriDTO>();
    }
}
