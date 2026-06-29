using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IAgentHistoryService
{
    Task<Guid> SaveAsync(AgentExecution execution, CancellationToken cancellationToken = default);
}
