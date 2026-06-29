using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Application.Dashboard;

public sealed class DashboardService : IDashboardService
{
    private readonly IApplicationDbContext _context;

    public DashboardService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto?> GetForUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(user => user.Id == userId)
            .Select(user => new DashboardDto(
                user.DisplayName ?? user.Email ?? "Usuario",
                user.Profession == null ? "Nao definida" : user.Profession.Name,
                user.Workspace == null ? "Workspace inicial" : user.Workspace.Name,
                0,
                0,
                0,
                0))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
