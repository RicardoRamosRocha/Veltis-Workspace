using Veltis.Workspace.Application.Forms.Interfaces;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FormVersioningService : IFormVersioningService
{
    public FormDefinition CreateNextVersion(FormDefinition current, string schemaJson, string? uiSchemaJson, string? validationJson)
    {
        return new FormDefinition
        {
            TenantId = current.TenantId,
            Name = current.Name,
            Description = current.Description,
            Version = current.Version + 1,
            JsonSchema = schemaJson,
            UiSchema = uiSchemaJson,
            SchemaJson = schemaJson,
            UiSchemaJson = uiSchemaJson,
            ValidationJson = validationJson,
            Category = current.Category,
            Icon = current.Icon,
            IsPublished = false
        };
    }

    public FormDefinition Duplicate(FormDefinition source, string name)
    {
        FormDefinition duplicate = Clone(source);
        duplicate.Name = name;
        duplicate.Version = 1;
        duplicate.IsPublished = false;
        return duplicate;
    }

    public FormDefinition Clone(FormDefinition source)
    {
        return new FormDefinition
        {
            TenantId = source.TenantId,
            Name = source.Name,
            Description = source.Description,
            Version = source.Version,
            JsonSchema = source.JsonSchema,
            UiSchema = source.UiSchema,
            SchemaJson = source.SchemaJson,
            UiSchemaJson = source.UiSchemaJson,
            ValidationJson = source.ValidationJson,
            Category = source.Category,
            Icon = source.Icon,
            IsPublished = source.IsPublished
        };
    }
}
