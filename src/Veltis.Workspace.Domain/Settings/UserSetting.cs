using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Settings;

public sealed class UserSetting : TenantEntity
{
    public Guid UserId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
}
