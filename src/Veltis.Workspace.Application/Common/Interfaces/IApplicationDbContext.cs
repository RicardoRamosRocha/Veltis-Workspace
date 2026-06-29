using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.Identity;

namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<ApplicationRole> Roles { get; }
    DbSet<Profession> Professions { get; }
    DbSet<UserProfession> UserProfessions { get; }
    DbSet<Veltis.Workspace.Domain.Entities.Workspace> Workspaces { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
