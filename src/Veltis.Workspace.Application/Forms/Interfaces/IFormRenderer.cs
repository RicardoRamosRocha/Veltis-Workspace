namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFormRenderer
{
    DynamicFormRenderModel Render(string schemaJson, string? uiSchemaJson = null, bool isPreview = false);
}
