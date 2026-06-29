using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Application.Permissions;

public sealed class PermissionService : IPermissionService
{
    private readonly IApplicationDbContext _context;

    public PermissionService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasPermissionAsync(Guid userId, string permissionKey, CancellationToken cancellationToken = default)
    {
        var roleIds = await _context.UserRoles
            .Where(userRole => userRole.UserId == userId)
            .Select(userRole => userRole.RoleId)
            .ToListAsync(cancellationToken);

        return await _context.RolePermissions
            .AnyAsync(rolePermission =>
                roleIds.Contains(rolePermission.RoleId)
                && rolePermission.Permission != null
                && rolePermission.Permission.Key == permissionKey
                && !rolePermission.IsDeleted,
                cancellationToken);
    }
}
