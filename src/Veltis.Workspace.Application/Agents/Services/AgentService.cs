using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Common.Results;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class AgentService : IAgentService
{
    private readonly IApplicationDbContext _context;
    private readonly IAgentExecutor _agentExecutor;

    public AgentService(IApplicationDbContext context, IAgentExecutor agentExecutor)
    {
        _context = context;
        _agentExecutor = agentExecutor;
    }

    public async Task<IReadOnlyCollection<AgentDto>> GetByProfessionAsync(Guid professionId, CancellationToken cancellationToken = default)
    {
        return await _context.Agents
            .Where(agent => agent.ProfessionId == professionId && agent.IsActive && !agent.IsDeleted)
            .OrderBy(agent => agent.DisplayOrder)
            .ThenBy(agent => agent.Name)
            .Select(agent => new AgentDto(
                agent.Id,
                agent.ProfessionId,
                agent.CategoryId,
                agent.Name,
                agent.Slug,
                agent.Description,
                agent.ExecutionMode,
                agent.IsActive))
            .ToListAsync(cancellationToken);
    }

    public Task<Result<AgentExecutionResult>> ExecuteAsync(AgentExecutionRequest request, CancellationToken cancellationToken = default)
    {
        return _agentExecutor.ExecuteAsync(request, cancellationToken);
    }
}
