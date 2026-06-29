using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Tests.Infrastructure;

public sealed class TestEntity : BaseEntity
{
    public string Name { get; set; } = "Test";
}
