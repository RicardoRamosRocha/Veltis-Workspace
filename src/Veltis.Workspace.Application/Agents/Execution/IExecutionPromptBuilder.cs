namespace Veltis.Workspace.Application.Agents.Execution;

public interface IExecutionPromptBuilder
{
    string Build(AgentExecutionRequest request, string systemPrompt, string instructions);
}