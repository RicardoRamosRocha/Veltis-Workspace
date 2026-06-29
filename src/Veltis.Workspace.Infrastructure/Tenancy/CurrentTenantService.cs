using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Infrastructure.Tenancy;

public sealed class CurrentTenantService : ITenantProvider
{
    public Guid? TenantId { get; private set; }
    public string? TenantSlug { get; private set; }

    public void SetTenant(Guid? tenantId, string? tenantSlug = null)
    {
        TenantId = tenantId;
        TenantSlug = tenantSlug;
    }
}
