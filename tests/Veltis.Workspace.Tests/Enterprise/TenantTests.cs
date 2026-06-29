using Veltis.Workspace.Domain.Entities;
using Veltis.Workspace.Domain.Tenancy;

namespace Veltis.Workspace.Tests.Enterprise;

public sealed class TenantTests
{
    [Fact]
    public void Tenant_Should_Start_Active()
    {
        var tenant = new Tenant();

        Assert.True(tenant.Active);
        Assert.NotEqual(Guid.Empty, tenant.Id);
    }

    [Fact]
    public void TenantEntity_Should_Accept_Null_Tenant_For_Global_Records()
    {
        var profession = new Profession();

        Assert.Null(profession.TenantId);
    }
}
