namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IValidationEngine
{
    FormValidationResult Validate(DynamicFormSchema schema, IReadOnlyDictionary<string, string?> values);
}
