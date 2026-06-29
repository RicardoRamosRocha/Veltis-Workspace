using Veltis.Workspace.Application.Forms.Interfaces;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FormRenderer : IFormRenderer
{
    private readonly IFormSchemaParser _schemaParser;
    private readonly IFieldFactory _fieldFactory;

    public FormRenderer(IFormSchemaParser schemaParser, IFieldFactory fieldFactory)
    {
        _schemaParser = schemaParser;
        _fieldFactory = fieldFactory;
    }

    public DynamicFormRenderModel Render(string schemaJson, string? uiSchemaJson = null, bool isPreview = false)
    {
        DynamicFormSchema schema = _schemaParser.Parse(schemaJson);

        return new DynamicFormRenderModel
        {
            FormId = schema.Id,
            Name = schema.Name,
            Description = schema.Description,
            Version = schema.Version,
            IsPreview = isPreview,
            Fields = schema.Fields
                .Where(field => field.Visible)
                .Select(_fieldFactory.Create)
                .ToArray(),
            Layout = schema.Layout
        };
    }
}
