using System.Text.Json;
using Veltis.Workspace.Application.Forms.Interfaces;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Forms.Services;

public sealed class FormImportExportService : IFormImportExportService
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public string Export(FormDefinition formDefinition)
    {
        return JsonSerializer.Serialize(formDefinition, Options);
    }

    public FormDefinition Import(string json)
    {
        FormDefinition? formDefinition = JsonSerializer.Deserialize<FormDefinition>(json, Options);

        return formDefinition ?? new FormDefinition();
    }
}
