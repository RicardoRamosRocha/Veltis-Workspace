using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Billing;

public sealed class Payment : TenantEntity
{
    public Guid InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public decimal Amount { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime? PaidAt { get; set; }
}
