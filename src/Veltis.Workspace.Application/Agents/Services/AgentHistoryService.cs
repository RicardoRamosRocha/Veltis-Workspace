using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class AgentHistoryService : IAgentHistoryService
{
    private readonly IApplicationDbContext _context;

    public AgentHistoryService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> SaveAsync(AgentExecution execution, CancellationToken cancellationToken = default)
    {
        _context.AgentExecutions.Add(execution);
        await _context.SaveChangesAsync(cancellationToken);
        return execution.Id;
    }
}
