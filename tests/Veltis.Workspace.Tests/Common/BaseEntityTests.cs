using Veltis.Workspace.Tests.Infrastructure;

namespace Veltis.Workspace.Tests.Common;

public sealed class BaseEntityTests
{
    [Fact]
    public void NewEntity_Should_Not_Be_Deleted()
    {
        var entity = new TestEntity();

        Assert.NotEqual(Guid.Empty, entity.Id);
        Assert.False(entity.IsDeleted);
        Assert.Null(entity.DeletedAt);
    }

    [Fact]
    public void MarkAsDeleted_Should_Set_SoftDelete_Fields()
    {
        var entity = new TestEntity();
        DateTime utcNow = DateTime.UtcNow;

        entity.MarkAsDeleted(utcNow);

        Assert.True(entity.IsDeleted);
        Assert.Equal(utcNow, entity.DeletedAt);
        Assert.Equal(utcNow, entity.UpdatedAt);
    }
}
