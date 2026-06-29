namespace Veltis.Workspace.Application.Common.Results;

public record Error(string Code, string Message)
{
    public static Error None => new(string.Empty, string.Empty);
    public static Error FromMessage(string message) => new("General.Error", message);
}
