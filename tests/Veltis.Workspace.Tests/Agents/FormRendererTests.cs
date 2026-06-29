using Veltis.Workspace.Application.Agents.Services;
using Veltis.Workspace.Application.Common.Results;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Tests.Agents;

public sealed class FormRendererTests
{
    [Fact]
    public void Validate_Should_Return_Failure_When_Required_Field_Is_Missing()
    {
        var renderer = new FormRenderer();
        var form = new FormDefinition
        {
            JsonSchema = """{"required":["title"]}"""
        };

        Result result = renderer.Validate(form, new Dictionary<string, string>());

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void Validate_Should_Return_Success_When_Required_Field_Exists()
    {
        var renderer = new FormRenderer();
        var form = new FormDefinition
        {
            JsonSchema = """{"required":["title"]}"""
        };

        Result result = renderer.Validate(form, new Dictionary<string, string> { ["title"] = "Documento" });

        Assert.True(result.IsSuccess);
    }
}
