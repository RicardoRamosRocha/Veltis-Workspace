using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Common;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.Identity;

namespace Veltis.Workspace.Infrastructure.Persistence;

public sealed class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Profession> Professions => Set<Profession>();
    public DbSet<UserProfession> UserProfessions => Set<UserProfession>();
    public DbSet<Veltis.Workspace.Domain.Entities.Workspace> Workspaces => Set<Veltis.Workspace.Domain.Entities.Workspace>();

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
                    Name = "Advogado",
                    Description = "Profissional juridico.",
                    Icon = "scale",
                    Color = "#0f766e",
                    Slug = "advogado",
                    Active = true,
                    CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = (DateTime?)null,
                    DeletedAt = (DateTime?)null,
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "Contador",
                    Description = "Profissional contabil e financeiro.",
                    Icon = "calculator",
                    Color = "#2563eb",
                    Slug = "contador",
                    Active = true,
                    CreatedAt = new DateTime(2026, 06, 29, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedAt = (DateTime?)null,
                    DeletedAt = (DateTime?)null,
                    IsDeleted = false
                },
                new
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Consultor",
                    Description = "Profissional de consultoria.",
                    Icon = "briefcase",
                    Color = "#7c3aed",
                    Slug = "consultor",
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

        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                entityType.FindProperty(nameof(BaseEntity.IsDeleted))?.SetDefaultValue(false);
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
}
