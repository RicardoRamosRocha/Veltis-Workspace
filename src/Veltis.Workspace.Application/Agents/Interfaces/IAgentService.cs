using Veltis.Workspace.Application.Common.Results;

namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IAgentService
{
    Task<IReadOnlyCollection<AgentDto>> GetByProfessionAsync(Guid professionId, CancellationToken cancellationToken = default);
    Task<Result<AgentExecutionResult>> ExecuteAsync(AgentExecutionRequest request, CancellationToken cancellationToken = default);
}
