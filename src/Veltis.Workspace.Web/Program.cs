using Veltis.Workspace.Application;
using Veltis.Workspace.Infrastructure;
using Veltis.Workspace.Infrastructure.Persistence;
using Veltis.Workspace.Web.Middleware;
using Veltis.Workspace.Web.Services;
using System.Threading.RateLimiting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Veltis.Workspace.Application.Common.Interfaces.ICurrentUserService, CurrentUserService>();
builder.Services.AddHealthChecks();
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 120,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

WebApplication app = builder.Build();

if (app.Configuration.GetValue<bool>("Seed:RunOnStartup"))
{
    await app.Services.SeedWorkspaceDatabaseAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseMiddleware<TenantResolutionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapHealthChecks("/ready");
app.MapHealthChecks("/live");
app.MapGet("/api/v1/meta", () => Results.Ok(new { name = "Veltis Workspace", version = "v1" }));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
