using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IAIModelSelector
{
    Task<AIModel?> SelectAsync(Agent agent, CancellationToken cancellationToken = default);
}
