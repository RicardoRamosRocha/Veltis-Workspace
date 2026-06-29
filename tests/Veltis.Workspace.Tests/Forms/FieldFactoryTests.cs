using Veltis.Workspace.Application.Forms;
using Veltis.Workspace.Application.Forms.Services;

namespace Veltis.Workspace.Tests.Forms;

public sealed class FieldFactoryTests
{
    [Fact]
    public void Create_ShouldApplyDefaultsAndNestedChildren()
    {
        DynamicFieldDefinition field = new()
        {
            Id = "section",
            Type = DynamicFieldType.Section,
            Label = "Section",
            Children =
            [
                new DynamicFieldDefinition { Id = "summary", Label = "Summary", Order = 1 }
            ]
        };

        DynamicFieldRenderModel model = new FieldFactory().Create(field);

        Assert.Equal("section", model.Name);
        Assert.Equal("full", model.Width);
        Assert.Single(model.Children);
        Assert.Equal("summary", model.Children[0].Name);
    }
}
