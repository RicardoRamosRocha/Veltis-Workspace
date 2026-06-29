namespace Veltis.Workspace.Application.Common.Results;

public sealed class Result<T> : Result
{
    private Result(bool isSuccess, T? value, IReadOnlyCollection<Error> errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(true, value, Array.Empty<Error>());
    }

    public new static Result<T> Failure(string message)
    {
        return Failure(Error.FromMessage(message));
    }

    public new static Result<T> Failure(Error error)
    {
        return new Result<T>(false, default, new[] { error });
    }

    public new static Result<T> Failure(IEnumerable<Error> errors)
    {
        Error[] errorArray = errors.ToArray();
        return new Result<T>(false, default, errorArray.Length == 0 ? new[] { Error.FromMessage("A operacao falhou.") } : errorArray);
    }
}
