namespace Veltis.Workspace.Application.Forms;

public sealed class FormValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public Dictionary<string, IReadOnlyList<string>> Errors { get; } = [];

    public void AddError(string fieldName, string message)
    {
        if (!Errors.TryGetValue(fieldName, out IReadOnlyList<string>? existing))
        {
            Errors[fieldName] = [message];
            return;
        }

        Errors[fieldName] = existing.Concat([message]).ToArray();
    }
}
