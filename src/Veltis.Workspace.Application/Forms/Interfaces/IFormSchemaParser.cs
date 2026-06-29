namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFormSchemaParser
{
    DynamicFormSchema Parse(string schemaJson);
}
