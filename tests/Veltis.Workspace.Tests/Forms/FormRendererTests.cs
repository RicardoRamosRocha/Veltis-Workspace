using Veltis.Workspace.Application.Forms.Services;

namespace Veltis.Workspace.Tests.Forms;

public sealed class FormRendererTests
{
    [Fact]
    public void Render_ShouldCreatePreviewModel()
    {
        const string schemaJson = """
        {
          "id": "preview-form",
          "name": "Preview Form",
          "fields": [
            { "id": "title", "name": "title", "type": "Textbox", "label": "Title", "visible": true }
          ]
        }
        """;

        FormRenderer renderer = new(new FormSchemaParser(), new FieldFactory());

        var model = renderer.Render(schemaJson, isPreview: true);

        Assert.True(model.IsPreview);
        Assert.Equal("Preview Form", model.Name);
        Assert.Single(model.Fields);
    }
}
