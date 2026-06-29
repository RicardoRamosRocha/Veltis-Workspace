using Microsoft.Extensions.DependencyInjection;
using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Agents.Services;
using Veltis.Workspace.Application.Dashboard;
using Veltis.Workspace.Application.FeatureFlags;
using Veltis.Workspace.Application.Notifications;
using Veltis.Workspace.Application.Permissions;
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
        services.AddScoped<Veltis.Workspace.Application.Common.Interfaces.IFeatureService, FeatureService>();
        services.AddScoped<Veltis.Workspace.Application.Common.Interfaces.IPermissionService, PermissionService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IAgentService, AgentService>();
        services.AddScoped<IAgentExecutor, AgentExecutor>();
        services.AddScoped<IAgentFactory, AgentFactory>();
        services.AddScoped<IPromptBuilder, PromptBuilder>();
        services.AddScoped<IPromptRenderer, PromptRenderer>();
        services.AddScoped<IAIModelSelector, AIModelSelector>();
        services.AddScoped<IFormRenderer, FormRenderer>();
        services.AddScoped<IAgentHistoryService, AgentHistoryService>();
        services.AddScoped<IDocumentGenerator, DocumentGenerator>();
        services.AddScoped<IAIProvider, ArchitectureOnlyAIProvider>();

        return services;
    }
}
