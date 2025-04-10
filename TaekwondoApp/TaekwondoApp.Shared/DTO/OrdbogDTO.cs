using SQLite;
using TaekwondoApp.Shared.Model;

namespace TaekwondoApp.Shared.DTO
{
    public class OrdbogDTO : SyncableEntity
    {
        [PrimaryKey] public Guid OrdbogId { get; set; }
        public string DanskOrd { get; set; }
        public string KoranskOrd { get; set; }
        public string Beskrivelse { get; set; }
        public string BilledeLink { get; set; }
        public string LydLink { get; set; }
        public string VideoLink { get; set; }
        public bool IsDeleted { get; set; } // Add IsDeleted flag
        public SyncStatus Status { get; set; } = SyncStatus.Pending;  // Default to Pending if it's not synced yet
    }
    public enum SyncStatus
    {
        Pending,
        Synced,
        Failed,
        Deleted
    }
}
