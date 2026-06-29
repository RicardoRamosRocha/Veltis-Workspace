using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Billing;

public sealed class Credit : TenantEntity
{
    public decimal Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
}
