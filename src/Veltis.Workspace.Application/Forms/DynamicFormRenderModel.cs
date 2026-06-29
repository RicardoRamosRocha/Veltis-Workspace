namespace Veltis.Workspace.Application.Forms;

public sealed class DynamicFormRenderModel
{
    public string FormId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Version { get; set; } = 1;
    public bool IsPreview { get; set; }
    public IReadOnlyList<DynamicFieldRenderModel> Fields { get; set; } = [];
    public IReadOnlyList<DynamicLayoutDefinition> Layout { get; set; } = [];
}
