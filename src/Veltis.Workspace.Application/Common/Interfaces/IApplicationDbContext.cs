using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Veltis.Workspace.Domain.Agents;
using Veltis.Workspace.Domain.Billing;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.FeatureFlags;
using Veltis.Workspace.Domain.Forms;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Domain.Notifications;
using Veltis.Workspace.Domain.Observability;
using Veltis.Workspace.Domain.Permissions;
using Veltis.Workspace.Domain.Settings;
using Veltis.Workspace.Domain.Tenancy;

namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<ApplicationRole> Roles { get; }
    DbSet<IdentityUserRole<Guid>> UserRoles { get; }
    DbSet<Profession> Professions { get; }
    DbSet<UserProfession> UserProfessions { get; }
    DbSet<Veltis.Workspace.Domain.Entities.Workspace> Workspaces { get; }
    DbSet<Tenant> Tenants { get; }
    DbSet<Feature> Features { get; }
    DbSet<FeatureFlag> FeatureFlags { get; }
    DbSet<PermissionGroup> PermissionGroups { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<Plan> Plans { get; }
    DbSet<Subscription> Subscriptions { get; }
    DbSet<Invoice> Invoices { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Usage> Usages { get; }
    DbSet<Credit> Credits { get; }
    DbSet<AuditLog> AuditLogs { get; }
    DbSet<SystemSetting> SystemSettings { get; }
    DbSet<UserSetting> UserSettings { get; }
    DbSet<WorkspaceSetting> WorkspaceSettings { get; }
    DbSet<FeatureSetting> FeatureSettings { get; }
    DbSet<Notification> Notifications { get; }
    DbSet<AgentCategory> AgentCategories { get; }
    DbSet<Agent> Agents { get; }
    DbSet<PromptTemplate> PromptTemplates { get; }
    DbSet<FormDefinition> FormDefinitions { get; }
    DbSet<FormSubmission> FormSubmissions { get; }
    DbSet<AIProvider> AIProviders { get; }
    DbSet<AIModel> AIModels { get; }
    DbSet<AgentExecution> AgentExecutions { get; }
    DbSet<GeneratedDocument> GeneratedDocuments { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
