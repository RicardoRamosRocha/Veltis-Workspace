using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Agents;

public sealed class AIModel : BaseEntity
{
    public Guid AIProviderId { get; set; }
    public AIProvider? AIProvider { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public int ContextWindow { get; set; }
    public bool SupportsVision { get; set; }
    public bool SupportsFunctions { get; set; }
    public bool SupportsStreaming { get; set; }
    public int MaxTokens { get; set; }
    public decimal InputPrice { get; set; }
    public decimal OutputPrice { get; set; }
    public bool IsActive { get; set; } = true;
}
