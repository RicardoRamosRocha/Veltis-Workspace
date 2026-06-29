namespace Veltis.Workspace.Application.Agents.Execution;

public interface IAgentExecutionPipeline
{
    Task<AgentExecutionResult> ExecuteAsync(
        AgentExecutionRequest request,
        CancellationToken cancellationToken = default);
}