using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Permissions;
using Veltis.Workspace.Domain.Permissions;

namespace Veltis.Workspace.Tests.Enterprise;

public sealed class PermissionServiceTests
{
    [Fact]
    public void PermissionService_Should_Implement_IPermissionService()
    {
        Assert.True(typeof(IPermissionService).IsAssignableFrom(typeof(PermissionService)));
    }

    [Fact]
    public void Permission_Should_Keep_Key()
    {
        var permission = new Permission { Key = "users.manage" };

        Assert.Equal("users.manage", permission.Key);
    }
}
