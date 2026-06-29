using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Billing;

public sealed class Plan : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public decimal MonthlyPrice { get; set; }
    public string Currency { get; set; } = "BRL";
    public bool Active { get; set; } = true;
}
