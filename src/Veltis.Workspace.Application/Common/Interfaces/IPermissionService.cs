namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IPermissionService
{
    Task<bool> HasPermissionAsync(Guid userId, string permissionKey, CancellationToken cancellationToken = default);
}
