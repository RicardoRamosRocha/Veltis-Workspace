namespace Veltis.Workspace.Application.Forms;

public sealed class DynamicFieldRenderModel
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DynamicFieldType Type { get; set; }
    public string Label { get; set; } = string.Empty;
    public string? Placeholder { get; set; }
    public string? HelpText { get; set; }
    public bool Required { get; set; }
    public bool Visible { get; set; } = true;
    public bool ReadOnly { get; set; }
    public string? DefaultValue { get; set; }
    public string CssClass { get; set; } = string.Empty;
    public string Width { get; set; } = "full";
    public IReadOnlyList<FieldOption> Options { get; set; } = [];
    public IReadOnlyList<DynamicFieldRenderModel> Children { get; set; } = [];
}
