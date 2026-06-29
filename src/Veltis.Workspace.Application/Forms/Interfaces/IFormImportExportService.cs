using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFormImportExportService
{
    string Export(FormDefinition formDefinition);
    FormDefinition Import(string json);
}
