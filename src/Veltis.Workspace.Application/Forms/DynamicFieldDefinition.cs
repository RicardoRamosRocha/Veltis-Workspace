namespace Veltis.Workspace.Application.Forms;

public sealed class DynamicFieldDefinition
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DynamicFieldType Type { get; set; } = DynamicFieldType.Textbox;
    public string Label { get; set; } = string.Empty;
    public string? Placeholder { get; set; }
    public string? HelpText { get; set; }
    public bool Required { get; set; }
    public bool Visible { get; set; } = true;
    public bool ReadOnly { get; set; }
    public string? DefaultValue { get; set; }
    public int? MinLength { get; set; }
    public int? MaxLength { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    public string? Mask { get; set; }
    public string? Validation { get; set; }
    public string? CssClass { get; set; }
    public int Order { get; set; }
    public string? Width { get; set; }
    public IReadOnlyList<FieldOption> Options { get; set; } = [];
    public IReadOnlyList<DynamicFieldDefinition> Children { get; set; } = [];
}
