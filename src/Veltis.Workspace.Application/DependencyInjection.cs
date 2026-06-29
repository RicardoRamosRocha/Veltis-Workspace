using Microsoft.Extensions.DependencyInjection;
using Veltis.Workspace.Application.Dashboard;
using Veltis.Workspace.Application.Professions;
using Veltis.Workspace.Application.Workspaces;

namespace Veltis.Workspace.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProfessionService, ProfessionService>();
        services.AddScoped<IWorkspaceService, WorkspaceService>();
        services.AddScoped<IDashboardService, DashboardService>();

        return services;
    }
}
