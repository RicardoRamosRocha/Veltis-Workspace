using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Billing;

public sealed class Subscription : TenantEntity
{
    public Guid PlanId { get; set; }
    public Plan? Plan { get; set; }
    public string Status { get; set; } = "Trial";
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndsAt { get; set; }
}
