using Veltis.Workspace.Infrastructure.Tenancy;

namespace Veltis.Workspace.Web.Middleware;

public sealed class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, CurrentTenantService tenantService)
    {
        Guid? tenantId = null;
        string? tenantSlug = context.Request.Headers["X-Tenant-Slug"].FirstOrDefault();

        string? tenantIdValue = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        if (Guid.TryParse(tenantIdValue, out Guid parsedTenantId))
        {
            tenantId = parsedTenantId;
        }

        tenantService.SetTenant(tenantId, tenantSlug);
        await _next(context);
    }
}
