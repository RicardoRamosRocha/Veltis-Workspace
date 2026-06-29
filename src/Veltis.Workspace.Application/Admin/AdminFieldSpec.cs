namespace Veltis.Workspace.Application.Admin;

public sealed class AdminFieldSpec
{
    public string Name { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public AdminFieldKind Kind { get; init; } = AdminFieldKind.Text;
    public bool Required { get; init; }
    public bool IsSlug { get; init; }
    public bool IsJson { get; init; }
    public decimal? MinDecimal { get; init; }
    public decimal? MaxDecimal { get; init; }
    public int? MinInt { get; init; }
}
