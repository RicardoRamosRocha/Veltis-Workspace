using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.FeatureFlags;

public sealed class FeatureFlag : TenantEntity
{
    public string FeatureKey { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
