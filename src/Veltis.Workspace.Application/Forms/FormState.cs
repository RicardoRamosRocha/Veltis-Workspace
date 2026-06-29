namespace Veltis.Workspace.Application.Forms;

public sealed class FormState
{
    public Dictionary<string, string?> Values { get; } = [];
    public Dictionary<string, IReadOnlyList<string>> Errors { get; } = [];
    public HashSet<string> Dirty { get; } = [];
    public HashSet<string> Touched { get; } = [];
    public HashSet<string> ReadOnly { get; } = [];
    public HashSet<string> Hidden { get; } = [];

    public bool IsValid => Errors.Count == 0;
}
