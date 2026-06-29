using Veltis.Workspace.Domain.Identity;

namespace Veltis.Workspace.Tests.Architecture;

public sealed class IdentityModelTests
{
    [Fact]
    public void ApplicationUser_Should_Be_Active_When_Created()
    {
        var user = new ApplicationUser();

        Assert.True(user.IsActive);
        Assert.NotEqual(Guid.Empty, user.Id);
    }

    [Fact]
    public void ApplicationRole_Should_Not_Be_SystemRole_By_Default()
    {
        var role = new ApplicationRole();

        Assert.False(role.IsSystemRole);
    }
}
