namespace TaekwondoApp.Shared.Models
{
    public abstract class SyncableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public ConflictResolutionStatus ConflictStatus { get; set; } = ConflictResolutionStatus.NoConflict; // Default to NoConflict if not resolved
        public SyncStatus Status { get; set; } = SyncStatus.Pending;  // Default to Pending if it's not synced yet
    }

    public enum ConflictResolutionStatus
    {
        NoConflict,
        ServerWins,
        LocalWins,
        ManualResolve
    }
    public enum SyncStatus
    {
        Pending,
        Synced,
        Failed,
        Deleted
    }
}
