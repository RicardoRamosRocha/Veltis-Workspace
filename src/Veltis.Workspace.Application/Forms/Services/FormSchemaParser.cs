using System.Text.Json;
using System.Text.Json.Serialization;
using Veltis.Workspace.Application.Forms.Interfaces;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FormSchemaParser : IFormSchemaParser
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    static FormSchemaParser()
    {
        Options.Converters.Add(new JsonStringEnumConverter());
    }

    public DynamicFormSchema Parse(string schemaJson)
    {
        if (string.IsNullOrWhiteSpace(schemaJson))
        {
            return new DynamicFormSchema();
        }

        DynamicFormSchema? schema = JsonSerializer.Deserialize<DynamicFormSchema>(schemaJson, Options);

        if (schema is null)
        {
            return new DynamicFormSchema();
        }

        return new DynamicFormSchema
        {
            Id = schema.Id,
            Name = schema.Name,
            Description = schema.Description,
            Version = schema.Version <= 0 ? 1 : schema.Version,
            Fields = schema.Fields
                .OrderBy(field => field.Order)
                .ThenBy(field => field.Label)
                .ToArray(),
            Layout = schema.Layout
                .OrderBy(layout => layout.Order)
                .ThenBy(layout => layout.Title)
                .ToArray()
        };
    }
}
