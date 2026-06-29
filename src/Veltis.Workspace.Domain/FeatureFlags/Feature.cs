using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.FeatureFlags;

public sealed class Feature : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool EnabledByDefault { get; set; }
}
