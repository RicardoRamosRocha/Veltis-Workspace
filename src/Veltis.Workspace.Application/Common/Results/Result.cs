namespace Veltis.Workspace.Application.Common.Results;

public class Result
{
    protected Result(bool isSuccess, IReadOnlyCollection<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyCollection<Error> Errors { get; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<Error>());
    }

    public static Result Failure(string message)
    {
        return Failure(Error.FromMessage(message));
    }

    public static Result Failure(Error error)
    {
        return new Result(false, new[] { error });
    }

    public static Result Failure(IEnumerable<Error> errors)
    {
        Error[] errorArray = errors.ToArray();
        return new Result(false, errorArray.Length == 0 ? new[] { Error.FromMessage("A operacao falhou.") } : errorArray);
    }
}
