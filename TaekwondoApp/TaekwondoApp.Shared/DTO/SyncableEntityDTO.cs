using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaekwondoApp.Shared.Models;

namespace TaekwondoApp.Shared.DTO
{
    public abstract class SyncableEntityDTO
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
        public ConflictResolutionStatus ConflictStatus { get; set; }
        public SyncStatus Status { get; set; }
        public int LastSyncedVersion { get; set; }
        public string ETag { get; set; }
        public string ModifiedBy { get; set; }
        public string ChangeHistoryJson { get; set; }
        public bool IsDeleted { get; set; }
    }

}
