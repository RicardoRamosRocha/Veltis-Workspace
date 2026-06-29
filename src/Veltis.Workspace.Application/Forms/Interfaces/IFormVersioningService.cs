using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFormVersioningService
{
    FormDefinition CreateNextVersion(FormDefinition current, string schemaJson, string? uiSchemaJson, string? validationJson);
    FormDefinition Duplicate(FormDefinition source, string name);
    FormDefinition Clone(FormDefinition source);
}
