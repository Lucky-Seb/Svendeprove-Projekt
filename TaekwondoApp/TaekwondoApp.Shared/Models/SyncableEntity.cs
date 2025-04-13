using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using SQLite;

namespace TaekwondoApp.Shared.Models
{
    public abstract class SyncableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public ConflictResolutionStatus ConflictStatus { get; set; } = ConflictResolutionStatus.NoConflict;  // Default to NoConflict if not resolved
        public SyncStatus Status { get; set; } = SyncStatus.Pending;  // Default to Pending if it's not synced yet

        // Versioning fields for conflict detection
        public int LastSyncedVersion { get; set; } = 0;  // Increment on each sync
        public string ETag { get; set; }  // Optional: String-based versioning (can be used with ETag header)

        // Tracking who or what modified the entity (user or device)
        public string ModifiedBy { get; set; }  // UserID or DeviceID

        // List of changes made to this entity (audit trail)
        [Ignore]  // SQLite will ignore this property when working with the database
        public List<ChangeRecord> ChangeHistory { get; set; } = new List<ChangeRecord>();

        // Property to store serialized ChangeHistory as a JSON string
        public string ChangeHistoryJson
        {
            get => JsonConvert.SerializeObject(ChangeHistory);
            set => ChangeHistory = string.IsNullOrEmpty(value)
                ? new List<ChangeRecord>()
                : JsonConvert.DeserializeObject<List<ChangeRecord>>(value);
        }

        public bool IsDeleted { get; set; } = false;  // Logical deletion flag
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

    public class ChangeRecord
    {
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; }  // User or device ID making the change
        public string ChangeDescription { get; set; }  // Description of what changed
    }
}
