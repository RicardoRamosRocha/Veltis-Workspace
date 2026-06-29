using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents;

public sealed record AgentDto(
    Guid Id,
    Guid ProfessionId,
    Guid CategoryId,
    string Name,
    string Slug,
    string? Description,
    AgentExecutionMode ExecutionMode,
    bool IsActive);
