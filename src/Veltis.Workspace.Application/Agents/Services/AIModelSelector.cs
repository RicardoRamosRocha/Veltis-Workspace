using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class AIModelSelector : IAIModelSelector
{
    private readonly IApplicationDbContext _context;

    public AIModelSelector(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AIModel?> SelectAsync(Agent agent, CancellationToken cancellationToken = default)
    {
        if (agent.DefaultModelId is Guid defaultModelId)
        {
            return await _context.AIModels
                .Include(model => model.AIProvider)
                .FirstOrDefaultAsync(model => model.Id == defaultModelId && model.IsActive, cancellationToken);
        }

        return await _context.AIModels
            .Include(model => model.AIProvider)
            .Where(model => model.IsActive)
            .OrderBy(model => model.InputPrice)
            .ThenBy(model => model.OutputPrice)
            .FirstOrDefaultAsync(cancellationToken);
    }
}
