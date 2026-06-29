using Veltis.Workspace.Application.Common.Results;

namespace Veltis.Workspace.Application.Workspaces;

public interface IWorkspaceService
{
    Task<WorkspaceDto?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<Guid>> EnsureForUserAsync(Guid userId, string displayName, CancellationToken cancellationToken = default);
}
