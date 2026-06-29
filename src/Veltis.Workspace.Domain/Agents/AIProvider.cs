using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class AIProvider : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
