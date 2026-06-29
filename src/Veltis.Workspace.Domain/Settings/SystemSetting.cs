using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Settings;

public sealed class SystemSetting : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
}
