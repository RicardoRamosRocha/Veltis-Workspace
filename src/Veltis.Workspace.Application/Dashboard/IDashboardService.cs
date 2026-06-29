namespace Veltis.Workspace.Application.Dashboard;

public interface IDashboardService
{
    Task<DashboardDto?> GetForUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
