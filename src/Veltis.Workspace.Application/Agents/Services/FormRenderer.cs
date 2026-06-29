using System.Text.Json;
using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Common.Results;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class FormRenderer : IFormRenderer
{
    public Result Validate(FormDefinition formDefinition, IReadOnlyDictionary<string, string> formData)
    {
        if (string.IsNullOrWhiteSpace(formDefinition.JsonSchema))
        {
            return Result.Success();
        }

        try
        {
            using JsonDocument document = JsonDocument.Parse(formDefinition.JsonSchema);

            if (!document.RootElement.TryGetProperty("required", out JsonElement required))
            {
                return Result.Success();
            }

            List<Error> errors = [];
            foreach (JsonElement field in required.EnumerateArray())
            {
                string? fieldName = field.GetString();
                if (!string.IsNullOrWhiteSpace(fieldName) && !formData.ContainsKey(fieldName))
                {
                    errors.Add(new ValidationError(fieldName, $"O campo '{fieldName}' e obrigatorio."));
                }
            }

            return errors.Count == 0 ? Result.Success() : Result.Failure(errors);
        }
        catch (JsonException)
        {
            return Result.Failure("Schema do formulario invalido.");
        }
    }
}
