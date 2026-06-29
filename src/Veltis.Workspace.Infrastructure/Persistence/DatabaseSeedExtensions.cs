using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Veltis.Workspace.Infrastructure.Persistence;

public static class DatabaseSeedExtensions
{
    public static async Task SeedWorkspaceDatabaseAsync(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken = default)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        ApplicationDbContextSeeder seeder = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSeeder>();
        ILogger<ApplicationDbContextSeeder> logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContextSeeder>>();

        try
        {
            await seeder.SeedAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Database seed failed.");
            throw;
        }
    }
}
