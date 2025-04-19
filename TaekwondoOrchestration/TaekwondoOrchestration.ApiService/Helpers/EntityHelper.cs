using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaekwondoApp.Shared.Models;

namespace TaekwondoOrchestration.ApiService.Helpers
{
    public static class EntityHelper
    {
        // Generate ETag based on entity properties. This can be used for any SyncableEntity.
        public static string GenerateETag<TEntity>(TEntity entity, params string[] properties) where TEntity : SyncableEntity
        {
            var etagSource = string.Join("-", properties);
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(etagSource));
        }

        // Initialize an entity with common fields. Works with any SyncableEntity.
        public static void InitializeEntity<TEntity>(TEntity entity, string modifiedBy, string changeDescription) where TEntity : SyncableEntity
        {
            entity.ETag = GenerateETag(entity, entity.GetType().Name, entity.CreatedAt.ToString(), entity.LastModified.ToString());
            entity.CreatedAt = DateTime.UtcNow;
            entity.LastModified = DateTime.UtcNow;
            entity.Status = SyncStatus.Synced;
            entity.ConflictStatus = ConflictResolutionStatus.NoConflict;
            entity.LastSyncedVersion = 0;
            entity.ModifiedBy = modifiedBy;
            entity.IsDeleted = false;
            entity.ChangeHistory = new List<ChangeRecord>
            {
                new ChangeRecord
                {
                    ChangedAt = DateTime.UtcNow,
                    ChangedBy = modifiedBy,
                    ChangeDescription = changeDescription
                }
            };
            entity.ChangeHistoryJson = JsonConvert.SerializeObject(entity.ChangeHistory);
        }

        // Set properties for deleted or restored entities (soft delete). Works with any SyncableEntity.
        public static void SetDeletedOrRestoredProperties<TEntity>(TEntity entity, string modifiedBy, string changeDescription) where TEntity : SyncableEntity
        {
            entity.IsDeleted = true;
            entity.LastModified = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            entity.ConflictStatus = ConflictResolutionStatus.NoConflict;
            entity.LastSyncedVersion++;
            entity.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = modifiedBy,
                ChangeDescription = changeDescription
            });
            entity.ChangeHistoryJson = JsonConvert.SerializeObject(entity.ChangeHistory);
            entity.ETag = GenerateETag(entity, entity.GetType().Name, entity.LastModified.ToString());
        }

        // Update common fields for an entity. Works with any SyncableEntity.
        public static void UpdateCommonFields<TEntity>(TEntity entity, string modifiedBy) where TEntity : SyncableEntity
        {
            entity.CreatedAt = entity.CreatedAt; // Preserve the original creation date.
            entity.LastModified = DateTime.UtcNow;
            entity.ETag = GenerateETag(entity, entity.GetType().Name, entity.LastModified.ToString());
            entity.ModifiedBy = modifiedBy;
            entity.LastSyncedVersion++;
            entity.ConflictStatus = ConflictResolutionStatus.NoConflict;

            entity.ChangeHistory.Add(new ChangeRecord
            {
                ChangedAt = DateTime.UtcNow,
                ChangedBy = modifiedBy,
                ChangeDescription = $"Updated entity with ID: {entity.ETag}"
            });

            entity.ChangeHistoryJson = JsonConvert.SerializeObject(entity.ChangeHistory);
        }
    }
}
