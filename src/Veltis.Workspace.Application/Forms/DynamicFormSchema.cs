namespace Veltis.Workspace.Application.Forms;

public sealed class DynamicFormSchema
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Version { get; set; } = 1;
    public IReadOnlyList<DynamicFieldDefinition> Fields { get; set; } = [];
    public IReadOnlyList<DynamicLayoutDefinition> Layout { get; set; } = [];
}
