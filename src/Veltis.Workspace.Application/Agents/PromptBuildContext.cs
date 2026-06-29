using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents;

public sealed class PromptBuildContext
{
    public Agent Agent { get; set; } = default!;
    public PromptTemplate PromptTemplate { get; set; } = default!;
    public string? ProfessionPrompt { get; set; }
    public IReadOnlyDictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    public IReadOnlyDictionary<string, string> UserPreferences { get; set; } = new Dictionary<string, string>();
    public IReadOnlyCollection<string> Rules { get; set; } = Array.Empty<string>();
}
