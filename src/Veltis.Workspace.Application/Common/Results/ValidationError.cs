namespace Veltis.Workspace.Application.Common.Results;

public sealed record ValidationError(string PropertyName, string Message)
    : Error("Validation.Error", Message);
