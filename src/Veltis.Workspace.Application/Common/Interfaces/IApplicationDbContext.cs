using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Domain.Identity;

namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<ApplicationRole> Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
