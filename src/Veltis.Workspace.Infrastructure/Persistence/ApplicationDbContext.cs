using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Agents;
using Veltis.Workspace.Domain.Billing;
using Veltis.Workspace.Domain.Common;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.FeatureFlags;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Domain.Notifications;
using Veltis.Workspace.Domain.Observability;
using Veltis.Workspace.Domain.Permissions;
using Veltis.Workspace.Domain.Settings;
using Veltis.Workspace.Domain.Tenancy;

namespace Veltis.Workspace.Infrastructure.Persistence;

public sealed class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
{
    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : this(options, new NullTenantProvider())
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public DbSet<Profession> Professions => Set<Profession>();
    public DbSet<UserProfession> UserProfessions => Set<UserProfession>();
    public DbSet<Veltis.Workspace.Domain.Entities.Workspace> Workspaces => Set<Veltis.Workspace.Domain.Entities.Workspace>();
    public new DbSet<IdentityUserRole<Guid>> UserRoles => Set<IdentityUserRole<Guid>>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<FeatureFlag> FeatureFlags => Set<FeatureFlag>();
    public DbSet<PermissionGroup> PermissionGroups => Set<PermissionGroup>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Usage> Usages => Set<Usage>();
    public DbSet<Credit> Credits => Set<Credit>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();
    public DbSet<UserSetting> UserSettings => Set<UserSetting>();
    public DbSet<WorkspaceSetting> WorkspaceSettings => Set<WorkspaceSetting>();
    public DbSet<FeatureSetting> FeatureSettings => Set<FeatureSetting>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AgentCategory> AgentCategories => Set<AgentCategory>();
    public DbSet<Agent> Agents => Set<Agent>();
    public DbSet<PromptTemplate> PromptTemplates => Set<PromptTemplate>();
    public DbSet<FormDefinition> FormDefinitions => Set<FormDefinition>();
    public DbSet<AIProvider> AIProviders => Set<AIProvider>();
    public DbSet<AIModel> AIModels => Set<AIModel>();
    public DbSet<AgentExecution> AgentExecutions => Set<AgentExecution>();
    public DbSet<GeneratedDocument> GeneratedDocuments => Set<GeneratedDocument>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(SystemConstants.DefaultSchema);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("users");
            entity.Property(user => user.DisplayName).HasMaxLength(ValidationConstants.DisplayNameMaxLength);
            entity.Property(user => user.CreatedAt).IsRequired();
            entity.Property(user => user.IsActive).HasDefaultValue(true);
            entity.HasOne(user => user.Profession)
                .WithMany()
                .HasForeignKey(user => user.ProfessionId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<ApplicationRole>(entity =>
        {
            entity.ToTable("roles");
            entity.Property(role => role.Description).HasMaxLength(ValidationConstants.RoleDescriptionMaxLength);
            entity.Property(role => role.IsSystemRole).HasDefaultValue(false);
        });

        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");

        builder.Entity<Profession>(entity =>
        {
            entity.ToTable("professions");
            entity.Property(profession => profession.Name).HasMaxLength(120).IsRequired();
            entity.Property(profession => profession.Description).HasMaxLength(500);
            entity.Property(profession => profession.Icon).HasMaxLength(80);
            entity.Property(profession => profession.Color).HasMaxLength(40);
            entity.Property(profession => profession.Slug).HasMaxLength(140).IsRequired();
            entity.Property(profession => profession.Active).HasDefaultValue(true);
            entity.HasIndex(profession => profession.Slug).IsUnique();
            entity.HasData(
                new
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Perfil Base",
                    Description = "Perfil profissional generico.",
                    Icon = "user",
                    Color = "#0f766e",
                    Slug = "Perfil Base",
                    Active = true,
                    CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = (DateTime?)null,
                    DeletedAt = (DateTime?)null,
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Gestor",
                    Description = "Perfil de gestao generico.",
                    Icon = "settings",
                    Color = "#2563eb",
                    Slug = "Gestor",
                    Active = true,
                    CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = (DateTime?)null,
                    DeletedAt = (DateTime?)null,
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Analista",
                    Description = "Profissional de Analistaia.",
                    Icon = "chart",
                    Color = "#7c3aed",
                    Slug = "Analista",
                    Active = true,
                    CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = (DateTime?)null,
                    DeletedAt = (DateTime?)null,
                    IsDeleted = false
                });
        });

        builder.Entity<UserProfession>(entity =>
        {
            entity.ToTable("user_professions");
            entity.HasIndex(userProfession => new { userProfession.UserId, userProfession.ProfessionId }).IsUnique();
            entity.HasOne(userProfession => userProfession.User)
                .WithMany(user => user.UserProfessions)
                .HasForeignKey(userProfession => userProfession.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(userProfession => userProfession.Profession)
                .WithMany(profession => profession.UserProfessions)
                .HasForeignKey(userProfession => userProfession.ProfessionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Veltis.Workspace.Domain.Entities.Workspace>(entity =>
        {
            entity.ToTable("workspaces");
            entity.Property(workspace => workspace.Name).HasMaxLength(160).IsRequired();
            entity.Property(workspace => workspace.Description).HasMaxLength(500);
            entity.HasIndex(workspace => workspace.UserId).IsUnique();
            entity.HasOne(workspace => workspace.User)
                .WithOne(user => user.Workspace)
                .HasForeignKey<Veltis.Workspace.Domain.Entities.Workspace>(workspace => workspace.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Tenant>(entity =>
        {
            entity.ToTable("tenants");
            entity.Property(tenant => tenant.Name).HasMaxLength(160).IsRequired();
            entity.Property(tenant => tenant.Slug).HasMaxLength(120).IsRequired();
            entity.Property(tenant => tenant.Region).HasMaxLength(80);
            entity.HasIndex(tenant => tenant.Slug).IsUnique();
        });

        builder.Entity<Feature>(entity =>
        {
            entity.ToTable("features");
            entity.Property(feature => feature.Key).HasMaxLength(120).IsRequired();
            entity.Property(feature => feature.Name).HasMaxLength(160).IsRequired();
            entity.Property(feature => feature.Description).HasMaxLength(500);
            entity.HasIndex(feature => feature.Key).IsUnique();
        });

        builder.Entity<FeatureFlag>(entity =>
        {
            entity.ToTable("feature_flags");
            entity.Property(flag => flag.FeatureKey).HasMaxLength(120).IsRequired();
            entity.HasIndex(flag => new { flag.TenantId, flag.FeatureKey }).IsUnique();
        });

        builder.Entity<PermissionGroup>(entity =>
        {
            entity.ToTable("permission_groups");
            entity.Property(group => group.Name).HasMaxLength(160).IsRequired();
            entity.Property(group => group.Key).HasMaxLength(120).IsRequired();
            entity.HasIndex(group => group.Key).IsUnique();
        });

        builder.Entity<Permission>(entity =>
        {
            entity.ToTable("permissions");
            entity.Property(permission => permission.Key).HasMaxLength(160).IsRequired();
            entity.Property(permission => permission.Name).HasMaxLength(160).IsRequired();
            entity.Property(permission => permission.Description).HasMaxLength(500);
            entity.HasIndex(permission => permission.Key).IsUnique();
            entity.HasOne(permission => permission.PermissionGroup)
                .WithMany(group => group.Permissions)
                .HasForeignKey(permission => permission.PermissionGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<RolePermission>(entity =>
        {
            entity.ToTable("role_permissions");
            entity.HasIndex(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId }).IsUnique();
            entity.HasOne(rolePermission => rolePermission.Permission)
                .WithMany()
                .HasForeignKey(rolePermission => rolePermission.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Plan>(entity =>
        {
            entity.ToTable("plans");
            entity.Property(plan => plan.Name).HasMaxLength(160).IsRequired();
            entity.Property(plan => plan.Slug).HasMaxLength(120).IsRequired();
            entity.Property(plan => plan.Currency).HasMaxLength(3).IsRequired();
            entity.HasIndex(plan => plan.Slug).IsUnique();
        });

        builder.Entity<Subscription>(entity =>
        {
            entity.ToTable("subscriptions");
            entity.Property(subscription => subscription.Status).HasMaxLength(40).IsRequired();
            entity.HasOne(subscription => subscription.Plan)
                .WithMany()
                .HasForeignKey(subscription => subscription.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Invoice>(entity =>
        {
            entity.ToTable("invoices");
            entity.Property(invoice => invoice.Number).HasMaxLength(80).IsRequired();
            entity.Property(invoice => invoice.Currency).HasMaxLength(3).IsRequired();
            entity.Property(invoice => invoice.Status).HasMaxLength(40).IsRequired();
        });

        builder.Entity<Payment>(entity =>
        {
            entity.ToTable("payments");
            entity.Property(payment => payment.Provider).HasMaxLength(80);
            entity.Property(payment => payment.Status).HasMaxLength(40).IsRequired();
        });

        builder.Entity<Usage>(entity =>
        {
            entity.ToTable("usages");
            entity.Property(usage => usage.MetricKey).HasMaxLength(120).IsRequired();
        });

        builder.Entity<Credit>(entity =>
        {
            entity.ToTable("credits");
            entity.Property(credit => credit.Reason).HasMaxLength(300);
        });

        builder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("audit_logs");
            entity.Property(log => log.IpAddress).HasMaxLength(80);
            entity.Property(log => log.Action).HasMaxLength(120).IsRequired();
            entity.Property(log => log.EntityName).HasMaxLength(160).IsRequired();
            entity.Property(log => log.EntityId).HasMaxLength(80);
        });

        builder.Entity<SystemSetting>(entity =>
        {
            entity.ToTable("system_settings");
            entity.Property(setting => setting.Key).HasMaxLength(160).IsRequired();
            entity.HasIndex(setting => setting.Key).IsUnique();
        });

        builder.Entity<UserSetting>(entity =>
        {
            entity.ToTable("user_settings");
            entity.Property(setting => setting.Key).HasMaxLength(160).IsRequired();
            entity.HasIndex(setting => new { setting.UserId, setting.Key }).IsUnique();
        });

        builder.Entity<WorkspaceSetting>(entity =>
        {
            entity.ToTable("workspace_settings");
            entity.Property(setting => setting.Key).HasMaxLength(160).IsRequired();
            entity.HasIndex(setting => new { setting.WorkspaceId, setting.Key }).IsUnique();
        });

        builder.Entity<FeatureSetting>(entity =>
        {
            entity.ToTable("feature_settings");
            entity.Property(setting => setting.FeatureKey).HasMaxLength(120).IsRequired();
            entity.Property(setting => setting.Key).HasMaxLength(160).IsRequired();
            entity.HasIndex(setting => new { setting.TenantId, setting.FeatureKey, setting.Key }).IsUnique();
        });

        builder.Entity<Notification>(entity =>
        {
            entity.ToTable("notifications");
            entity.Property(notification => notification.Title).HasMaxLength(160).IsRequired();
            entity.Property(notification => notification.Message).HasMaxLength(1000).IsRequired();
        });

        builder.Entity<AgentCategory>(entity =>
        {
            entity.ToTable("agent_categories");
            entity.Property(category => category.Name).HasMaxLength(160).IsRequired();
            entity.Property(category => category.Slug).HasMaxLength(160).IsRequired();
            entity.Property(category => category.Description).HasMaxLength(500);
            entity.Property(category => category.Icon).HasMaxLength(80);
            entity.HasIndex(category => new { category.TenantId, category.Slug }).IsUnique();
        });

        builder.Entity<PromptTemplate>(entity =>
        {
            entity.ToTable("prompt_templates");
            entity.Property(template => template.SystemPrompt).IsRequired();
            entity.Property(template => template.Language).HasMaxLength(20).IsRequired();
            entity.Property(template => template.Tone).HasMaxLength(80);
        });

        builder.Entity<FormDefinition>(entity =>
        {
            entity.ToTable("form_definitions");
            entity.Property(form => form.Name).HasMaxLength(160).IsRequired();
            entity.Property(form => form.Description).HasMaxLength(500);
            entity.Property(form => form.JsonSchema).IsRequired();
        });

        builder.Entity<AIProvider>(entity =>
        {
            entity.ToTable("ai_providers");
            entity.Property(provider => provider.Name).HasMaxLength(120).IsRequired();
            entity.Property(provider => provider.Key).HasMaxLength(120).IsRequired();
            entity.HasIndex(provider => provider.Key).IsUnique();
            entity.HasData(new
            {
                Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"),
                Name = "Architecture Only",
                Key = "architecture-only",
                IsActive = true,
                CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = (DateTime?)null,
                DeletedAt = (DateTime?)null,
                IsDeleted = false
            });
        });

        builder.Entity<AIModel>(entity =>
        {
            entity.ToTable("ai_models");
            entity.Property(model => model.Provider).HasMaxLength(120).IsRequired();
            entity.Property(model => model.ModelName).HasMaxLength(160).IsRequired();
            entity.HasOne(model => model.AIProvider)
                .WithMany()
                .HasForeignKey(model => model.AIProviderId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasData(new
            {
                Id = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002"),
                AIProviderId = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001"),
                Provider = "architecture-only",
                ModelName = "architecture-only-model",
                ContextWindow = 8192,
                SupportsVision = false,
                SupportsFunctions = false,
                SupportsStreaming = false,
                MaxTokens = 2048,
                InputPrice = 0m,
                OutputPrice = 0m,
                IsActive = true,
                CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                UpdatedAt = (DateTime?)null,
                DeletedAt = (DateTime?)null,
                IsDeleted = false
            });
        });

        builder.Entity<Agent>(entity =>
        {
            entity.ToTable("agents");
            entity.Property(agent => agent.Name).HasMaxLength(160).IsRequired();
            entity.Property(agent => agent.Slug).HasMaxLength(160).IsRequired();
            entity.Property(agent => agent.Description).HasMaxLength(500);
            entity.Property(agent => agent.Icon).HasMaxLength(80);
            entity.Property(agent => agent.CoverImage).HasMaxLength(300);
            entity.HasIndex(agent => new { agent.TenantId, agent.ProfessionId, agent.Slug }).IsUnique();
            entity.HasOne(agent => agent.Profession)
                .WithMany()
                .HasForeignKey(agent => agent.ProfessionId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(agent => agent.Category)
                .WithMany()
                .HasForeignKey(agent => agent.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(agent => agent.PromptTemplate)
                .WithMany()
                .HasForeignKey(agent => agent.PromptTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(agent => agent.FormDefinition)
                .WithMany()
                .HasForeignKey(agent => agent.FormDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(agent => agent.DefaultModel)
                .WithMany()
                .HasForeignKey(agent => agent.DefaultModelId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<AgentExecution>(entity =>
        {
            entity.ToTable("agent_executions");
            entity.Property(execution => execution.Prompt).IsRequired();
            entity.Property(execution => execution.Response);
            entity.Property(execution => execution.Provider).HasMaxLength(120);
            entity.Property(execution => execution.Model).HasMaxLength(160);
            entity.HasOne(execution => execution.Agent)
                .WithMany()
                .HasForeignKey(execution => execution.AgentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<GeneratedDocument>(entity =>
        {
            entity.ToTable("generated_documents");
            entity.Property(document => document.Title).HasMaxLength(200).IsRequired();
            entity.Property(document => document.Content).IsRequired();
            entity.Property(document => document.Format).HasMaxLength(40).IsRequired();
            entity.HasOne(document => document.Execution)
                .WithMany()
                .HasForeignKey(document => document.ExecutionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.FindProperty(nameof(BaseEntity.IsDeleted))?.SetDefaultValue(false);
            }

            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                SetTenantQueryFilter(builder, entityType.ClrType);
            }
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        ApplyAuditInformation();
        return base.SaveChanges();
    }

    private void ApplyAuditInformation()
    {
        DateTime utcNow = DateTime.UtcNow;

        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.MarkAsCreated(utcNow);
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.MarkAsUpdated(utcNow);
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.MarkAsDeleted(utcNow);
            }
        }
    }

    private void SetTenantQueryFilter(ModelBuilder builder, Type entityType)
    {
        typeof(ApplicationDbContext)
            .GetMethod(nameof(SetTenantQueryFilter), 1, new[] { typeof(ModelBuilder) })?
            .MakeGenericMethod(entityType)
            .Invoke(this, new object[] { builder });
    }

    private void SetTenantQueryFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, ITenantEntity
    {
        Expression<Func<TEntity, bool>> filter = entity =>
            _tenantProvider.TenantId == null || entity.TenantId == null || entity.TenantId == _tenantProvider.TenantId;

        builder.Entity<TEntity>().HasQueryFilter(filter);
    }

    private sealed class NullTenantProvider : ITenantProvider
    {
        public Guid? TenantId => null;
        public string? TenantSlug => null;
    }
}

