using Veltis.Workspace.Application.Common.Results;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IFormRenderer
{
    Result Validate(FormDefinition formDefinition, IReadOnlyDictionary<string, string> formData);
}
