using Veltis.Workspace.Application.Forms.Services;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Tests.Forms;

public sealed class FormVersioningTests
{
    [Fact]
    public void CreateNextVersion_ShouldIncrementVersionAndKeepCurrentUnchanged()
    {
        FormDefinition current = new()
        {
            Name = "Professional Form",
            Version = 3,
            SchemaJson = """{"name":"v3"}""",
            JsonSchema = """{"name":"v3"}""",
            IsPublished = true
        };

        FormDefinition next = new FormVersioningService()
            .CreateNextVersion(current, """{"name":"v4"}""", "{}", "{}");

        Assert.Equal(3, current.Version);
        Assert.Equal(4, next.Version);
        Assert.False(next.IsPublished);
        Assert.Equal("""{"name":"v4"}""", next.SchemaJson);
    }

    [Fact]
    public void Duplicate_ShouldResetVersionAndPublication()
    {
        FormDefinition source = new()
        {
            Name = "Source",
            Version = 5,
            IsPublished = true
        };

        FormDefinition duplicate = new FormVersioningService().Duplicate(source, "Copy");

        Assert.Equal("Copy", duplicate.Name);
        Assert.Equal(1, duplicate.Version);
        Assert.False(duplicate.IsPublished);
    }
}
