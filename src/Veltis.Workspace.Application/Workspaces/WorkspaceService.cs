using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Common.Results;
using WorkspaceEntity = Veltis.Workspace.Domain.Entities.Workspace;

namespace Veltis.Workspace.Application.Workspaces;

public sealed class WorkspaceService : IWorkspaceService
{
    private readonly IApplicationDbContext _context;

    public WorkspaceService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<WorkspaceDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Workspaces
            .Where(workspace => workspace.UserId == userId && !workspace.IsDeleted)
            .Select(workspace => new WorkspaceDto(workspace.Id, workspace.UserId, workspace.Name, workspace.Description))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Result<Guid>> EnsureForUserAsync(Guid userId, string displayName, CancellationToken cancellationToken = default)
    {
        WorkspaceEntity? existingWorkspace = await _context.Workspaces
            .FirstOrDefaultAsync(workspace => workspace.UserId == userId && !workspace.IsDeleted, cancellationToken);

        if (existingWorkspace is not null)
        {
            return Result<Guid>.Success(existingWorkspace.Id);
        }

        var workspace = new WorkspaceEntity
        {
            UserId = userId,
            Name = $"Workspace de {displayName}",
            Description = "Espaco inicial de trabalho."
        };

        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(workspace.Id);
    }
}
