using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class FormDefinition : TenantEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string JsonSchema { get; set; } = "{}";
    public string? UiSchema { get; set; }
    public int Version { get; set; } = 1;
}
