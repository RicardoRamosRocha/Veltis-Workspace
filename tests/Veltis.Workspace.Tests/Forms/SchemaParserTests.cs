using Veltis.Workspace.Application.Forms;
using Veltis.Workspace.Application.Forms.Services;

namespace Veltis.Workspace.Tests.Forms;

public sealed class SchemaParserTests
{
    [Fact]
    public void Parse_ShouldReadFieldsAndOrderThem()
    {
        const string schemaJson = """
        {
          "id": "professional-form",
          "name": "Professional Form",
          "version": 2,
          "fields": [
            { "id": "email", "name": "email", "type": "Email", "label": "Email", "order": 2 },
            { "id": "name", "name": "name", "type": "Textbox", "label": "Name", "order": 1 }
          ]
        }
        """;

        DynamicFormSchema schema = new FormSchemaParser().Parse(schemaJson);

        Assert.Equal("professional-form", schema.Id);
        Assert.Equal(2, schema.Version);
        Assert.Equal("name", schema.Fields.First().Name);
        Assert.Equal(DynamicFieldType.Email, schema.Fields.Last().Type);
    }
}
