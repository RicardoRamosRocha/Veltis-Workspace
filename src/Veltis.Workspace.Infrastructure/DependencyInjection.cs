using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Veltis.Workspace.Application.Common.Events;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Constants;
using Veltis.Workspace.Domain.Identity;
using Veltis.Workspace.Application.AI;
using Veltis.Workspace.Infrastructure.AI.OpenAI;
using Veltis.Workspace.Infrastructure.Events;
using Veltis.Workspace.Infrastructure.Persistence;
using Veltis.Workspace.Infrastructure.Services;
using Veltis.Workspace.Infrastructure.Tenancy;
using Veltis.Workspace.Infrastructure.AI.Gemini;

namespace Veltis.Workspace.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

        services.AddMemoryCache();
        services.Configure<AiProviderOptions>(configuration.GetSection("AI"));
        services.Configure<OpenAiOptions>(configuration.GetSection("AI:OpenAI"));

        var defaultProvider = configuration["AI:DefaultProvider"];

        if (string.Equals(defaultProvider, "Gemini", StringComparison.OrdinalIgnoreCase))
        {
            services.AddHttpClient<IAiChatProvider, GeminiChatProvider>(client =>
            {
                client.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
            });
        }
        else
        {
            services.AddHttpClient<IAiChatProvider, OpenAiChatProvider>((provider, client) =>
            {
                AiProviderOptions options = provider.GetRequiredService<Microsoft.Extensions.Options.IOptions<AiProviderOptions>>().Value;
                string baseUrl = string.IsNullOrWhiteSpace(options.OpenAI.BaseUrl)
                    ? "https://api.openai.com/v1"
                    : options.OpenAI.BaseUrl.TrimEnd('/');

                client.BaseAddress = new Uri(baseUrl + "/");
            });
        }

        services.AddScoped<CurrentTenantService>();
        services.AddScoped<ITenantProvider>(provider => provider.GetRequiredService<CurrentTenantService>());

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ApplicationDbContextSeeder>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        services.AddScoped<IStorageService, LocalStorageService>();
        services.AddScoped<IFileStorageService, LocalStorageService>();
        services.AddScoped<IEmailService, SmtpEmailService>();
        services.AddScoped<IEmailSender, SmtpEmailService>();
        services.AddSingleton<IJobQueue, InMemoryJobQueue>();
        services.AddScoped<IEventPublisher, InProcessEventPublisher>();

        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/account/login";
            options.LogoutPath = "/account/logout";
            options.AccessDeniedPath = "/account/access-denied";
            options.Cookie.Name = SystemConstants.AuthenticationCookieName;
            options.SlidingExpiration = true;
        });

        return services;
    }
}
