using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class FormDefinition : TenantEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string JsonSchema { get; set; } = "{}";
    public string? UiSchema { get; set; }
    public int Version { get; set; } = 1;
    public string SchemaJson { get; set; } = "{}";
    public string? UiSchemaJson { get; set; }
    public string? ValidationJson { get; set; }
    public string? Category { get; set; }
    public string? Icon { get; set; }
    public bool IsPublished { get; set; }
}
