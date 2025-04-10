namespace TaekwondoApp.Shared.Model
{
    public abstract class SyncableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public ConflictResolutionStatus ConflictStatus { get; set; }
    }

    public enum ConflictResolutionStatus
    {
        NoConflict,
        ServerWins,
        LocalWins,
        ManualResolve
    }
}
