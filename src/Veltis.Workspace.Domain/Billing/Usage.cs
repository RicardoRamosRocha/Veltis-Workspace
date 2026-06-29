using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Billing;

public sealed class Usage : TenantEntity
{
    public string MetricKey { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}
