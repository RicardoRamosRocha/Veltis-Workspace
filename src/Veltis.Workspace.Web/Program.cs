using Veltis.Workspace.Application;
using Veltis.Workspace.Infrastructure;
using Veltis.Workspace.Infrastructure.Persistence;
using Veltis.Workspace.Web.Middleware;
using Veltis.Workspace.Web.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Veltis.Workspace.Application.Common.Interfaces.ICurrentUserService, CurrentUserService>();

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
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
