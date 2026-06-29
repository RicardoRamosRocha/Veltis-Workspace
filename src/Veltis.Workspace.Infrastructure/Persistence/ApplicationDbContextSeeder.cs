using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Infrastructure.Identity;

namespace Veltis.Workspace.Infrastructure.Persistence;

public sealed class ApplicationDbContextSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApplicationDbContextSeeder> _logger;

    public ApplicationDbContextSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IConfiguration configuration,
        ILogger<ApplicationDbContextSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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
