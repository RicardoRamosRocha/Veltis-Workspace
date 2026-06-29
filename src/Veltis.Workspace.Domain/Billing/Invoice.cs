using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Billing;

public sealed class Invoice : TenantEntity
{
    public Guid SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "BRL";
    public string Status { get; set; } = "Draft";
    public DateTime DueAt { get; set; }
}
