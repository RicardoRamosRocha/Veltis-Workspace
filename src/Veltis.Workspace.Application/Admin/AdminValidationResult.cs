namespace Veltis.Workspace.Application.Admin;

public sealed class AdminValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public Dictionary<string, List<string>> Errors { get; } = [];

    public void Add(string fieldName, string message)
    {
        if (!Errors.TryGetValue(fieldName, out List<string>? fieldErrors))
        {
            fieldErrors = [];
            Errors[fieldName] = fieldErrors;
        }

        fieldErrors.Add(message);
    }
}
