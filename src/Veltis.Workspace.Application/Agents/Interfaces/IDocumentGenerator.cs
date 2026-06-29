using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IDocumentGenerator
{
    Task<GeneratedDocument> GenerateAsync(AgentExecution execution, string title, CancellationToken cancellationToken = default);
}
