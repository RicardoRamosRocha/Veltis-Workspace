using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class PromptTemplate : TenantEntity
{
    public string SystemPrompt { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public string? Variables { get; set; }
    public string? OutputFormat { get; set; }
    public string Language { get; set; } = "pt-BR";
    public string? Tone { get; set; }
    public int Version { get; set; } = 1;
}
