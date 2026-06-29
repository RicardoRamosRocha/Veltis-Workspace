using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Observability;

public sealed class AuditLog : TenantEntity
{
    public Guid? UserId { get; set; }
    public string? IpAddress { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
}
