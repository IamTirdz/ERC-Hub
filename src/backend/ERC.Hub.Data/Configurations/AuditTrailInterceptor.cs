using ERC.Hub.Common.Enums;
using ERC.Hub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace ERC.Hub.Data.Configurations
{
    public sealed class AuditTrailInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, 
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                AddAuditableLogs(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddAuditableLogs(DbContext context)
        {
            Dictionary<string, object> OldValues = [];
            Dictionary<string, object> NewValues = [];
            List<string> ChangedColumns = [];
            List<EntityEntry> entities = [];
            AuditTrail auditLog = new();

            context.ChangeTracker.DetectChanges();
            var entries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added
                    || e.State == EntityState.Modified
                    || e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                auditLog.Entity = entry.Entity.GetType().Name;

                var propertyId = entry.Entity.GetType().GetProperty("Id");
                if (propertyId != null)
                    auditLog.EntityId = propertyId.GetValue(entry.Entity)!.ToString()!;

                foreach (var property in entry.OriginalValues.Properties)
                {
                    var entity = (EntityBase)entry.Entity;
                    var originalValue = entry.OriginalValues[property];
                    var currentValue = entry.CurrentValues[property];

                    if (property.Name == "CreatedAt" || property.Name == "ModifiedAt")
                        continue;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                        auditLog.Action = AuditType.Create.ToString();
                        NewValues[property.Name] = currentValue!;
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property("CreatedAt").IsModified = false;
                        entity.ModifiedAt = DateTime.UtcNow;

                        auditLog.Action = AuditType.Update.ToString();
                        ChangedColumns.Add(property.Name);
                        OldValues[property.Name] = originalValue!;
                        NewValues[property.Name] = currentValue!;
                    }

                    if (entry.State == EntityState.Deleted)
                    {
                        auditLog.Action = AuditType.Delete.ToString();
                        OldValues[property.Name] = originalValue!;
                    }
                }
            }

            auditLog.ReferenceKey = Guid.NewGuid();
            auditLog.Timestamp = DateTime.UtcNow;
            auditLog.Columns = ChangedColumns.Count == 0 ? default! : JsonSerializer.Serialize(ChangedColumns);
            auditLog.OldValues = OldValues.Count == 0 ? default! : JsonSerializer.Serialize(OldValues);
            auditLog.NewValues = NewValues.Count == 0 ? default! : JsonSerializer.Serialize(NewValues);

            context.Set<AuditTrail>().Add(auditLog);
        }
    }
}
