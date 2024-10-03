namespace ERC.Hub.Data.Models
{
    public class AuditTrail
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string? Columns { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }        
        public long? UserId { get; set; }
        public virtual Employee? User { get; set; }
        public Guid ReferenceKey { get; set; }
    }
}
