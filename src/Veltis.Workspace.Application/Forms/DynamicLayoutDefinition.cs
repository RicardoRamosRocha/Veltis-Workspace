namespace Veltis.Workspace.Application.Forms;

public sealed class DynamicLayoutDefinition
{
    public string Id { get; set; } = string.Empty;
    public DynamicLayoutType Type { get; set; } = DynamicLayoutType.Section;
    public string? Title { get; set; }
    public int Order { get; set; }
    public IReadOnlyList<string> Fields { get; set; } = [];
}
