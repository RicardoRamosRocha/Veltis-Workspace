using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Settings;

public sealed class FeatureSetting : TenantEntity
{
    public string FeatureKey { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
}
