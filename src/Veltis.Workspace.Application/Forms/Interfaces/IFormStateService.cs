namespace Veltis.Workspace.Application.Forms.Interfaces;

public interface IFormStateService
{
    FormState Create(DynamicFormSchema schema, IReadOnlyDictionary<string, string?>? values = null);
    FormState Touch(FormState state, string fieldName);
    FormState SetValue(FormState state, string fieldName, string? value);
}
