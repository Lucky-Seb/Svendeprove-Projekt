using SQLite;

namespace TaekwondoApp.Shared.DTO
{

    public class OrdbogDTO
    {
        [PrimaryKey] public int Id { get; set; }
        public string DanskOrd { get; set; }
        public string KoranskOrd { get; set; }
        public string Beskrivelse { get; set; }
        public string BilledeLink { get; set; }
        public string LydLink { get; set; }
        public string VideoLink { get; set; }
        public bool IsSync { get; set; } = true;  // Default to true when syncing from the server
    }

}
