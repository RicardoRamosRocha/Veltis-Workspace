using Veltis.Workspace.Application.Common.Results;

namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IAgentExecutor
{
    Task<Result<AgentExecutionResult>> ExecuteAsync(AgentExecutionRequest request, CancellationToken cancellationToken = default);
}
