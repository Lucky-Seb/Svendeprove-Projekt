using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite;

namespace TaekwondoApp.Shared.Models
{
    public abstract class SyncableEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public ConflictResolutionStatus ConflictStatus { get; set; } = ConflictResolutionStatus.NoConflict;
        public SyncStatus Status { get; set; } = SyncStatus.Pending;

        public int LastSyncedVersion { get; set; } = 0;
        public string ETag { get; set; }
        public string ModifiedBy { get; set; }

        [Ignore]
        public List<ChangeRecord> ChangeHistory { get; set; } = new List<ChangeRecord>();

        public string ChangeHistoryJson
        {
            get => JsonConvert.SerializeObject(ChangeHistory);
            set => ChangeHistory = string.IsNullOrEmpty(value)
                ? new List<ChangeRecord>()
                : JsonConvert.DeserializeObject<List<ChangeRecord>>(value);
        }

        public bool IsDeleted { get; set; } = false;

        // 🧠 Generic primary key getter for reflection-based ID resolution
        public Guid GetPrimaryKey()
        {
            var prop = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p =>
                    Attribute.IsDefined(p, typeof(PrimaryKeyAttribute)) &&
                    p.PropertyType == typeof(Guid));

            if (prop != null)
                return (Guid)prop.GetValue(this);

            throw new InvalidOperationException($"No [PrimaryKey] Guid property found on {this.GetType().Name}");
        }
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
        public string ChangedBy { get; set; }
        public string ChangeDescription { get; set; }
    }
}
