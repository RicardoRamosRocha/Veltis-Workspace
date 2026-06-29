using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.FeatureFlags;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Domain.Permissions;
using Veltis.Workspace.Infrastructure.Identity;

namespace Veltis.Workspace.Infrastructure.Persistence;

public sealed class ApplicationDbContextSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApplicationDbContextSeeder> _logger;

    public ApplicationDbContextSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext context,
        IConfiguration configuration,
        ILogger<ApplicationDbContextSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        foreach (string roleName in ApplicationRoles.All)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = roleName,
                    Description = $"Role padrao {roleName}",
                    IsSystemRole = true
                });
            }
        }

        if (!_context.Professions.Any())
        {
            _context.Professions.AddRange(
                new Profession
                {
                    Name = "Perfil Base",
                    Description = "Perfil profissional generico.",
                    Icon = "user",
                    Color = "#0f766e",
                    Slug = "Perfil Base"
                },
                new Profession
                {
                    Name = "Gestor",
                    Description = "Perfil de gestao generico.",
                    Icon = "settings",
                    Color = "#2563eb",
                    Slug = "Gestor"
                },
                new Profession
                {
                    Name = "Analista",
                    Description = "Profissional de Analistaia.",
                    Icon = "chart",
                    Color = "#7c3aed",
                    Slug = "Analista"
                });

            await _context.SaveChangesAsync(cancellationToken);
        }

        if (!_context.Features.Any())
        {
            _context.Features.AddRange(
                new Feature { Key = "ai", Name = "IA", EnabledByDefault = false },
                new Feature { Key = "marketplace", Name = "Marketplace", EnabledByDefault = false },
                new Feature { Key = "community", Name = "Comunidade", EnabledByDefault = false },
                new Feature { Key = "pdf-export", Name = "Exportacao PDF", EnabledByDefault = false },
                new Feature { Key = "word-export", Name = "Exportacao Word", EnabledByDefault = false },
                new Feature { Key = "multiple-workspaces", Name = "Multiplos Workspaces", EnabledByDefault = false },
                new Feature { Key = "api", Name = "API", EnabledByDefault = false },
                new Feature { Key = "mobile-app", Name = "Aplicativo Mobile", EnabledByDefault = false });

            await _context.SaveChangesAsync(cancellationToken);
        }

        if (!_context.PermissionGroups.Any())
        {
            var administration = new PermissionGroup
            {
                Name = "Administracao",
                Key = "administration"
            };

            administration.Permissions.Add(new Permission
            {
                Name = "Gerenciar usuarios",
                Key = PermissionConstants.ManageUsers
            });
            administration.Permissions.Add(new Permission
            {
                Name = "Gerenciar configuracoes",
                Key = PermissionConstants.ManageSettings
            });

            _context.PermissionGroups.Add(administration);
            await _context.SaveChangesAsync(cancellationToken);
        }

        var options = new IdentitySeederOptions();
        _configuration.GetSection(IdentitySeederOptions.SectionName).Bind(options);

        if (string.IsNullOrWhiteSpace(options.Email) || string.IsNullOrWhiteSpace(options.Password))
        {
            _logger.LogInformation("Admin seed skipped because Seed:AdminUser is not configured.");
            return;
        }

        ApplicationUser? existingAdmin = await _userManager.FindByEmailAsync(options.Email);
        if (existingAdmin is not null)
        {
            return;
        }

        var admin = new ApplicationUser
        {
            Email = options.Email,
            UserName = options.Email,
            DisplayName = string.IsNullOrWhiteSpace(options.DisplayName) ? "Administrator" : options.DisplayName
        };

        IdentityResult result = await _userManager.CreateAsync(admin, options.Password);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Admin seed failed: {Errors}", string.Join("; ", result.Errors.Select(error => error.Description)));
            return;
        }

        await _userManager.AddToRoleAsync(admin, ApplicationRoles.Administrator);
    }
}

