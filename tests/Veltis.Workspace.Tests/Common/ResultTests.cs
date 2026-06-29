using Veltis.Workspace.Application.Common.Results;

namespace Veltis.Workspace.Tests.Common;

public sealed class ResultTests
{
    [Fact]
    public void Success_Should_Create_Successful_Result()
    {
        Result result = Result.Success();

        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Failure_Should_Create_Failed_Result_With_Message()
    {
        Result result = Result.Failure("Erro esperado.");

        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
    }

    [Fact]
    public void GenericSuccess_Should_Carry_Value()
    {
        Result<string> result = Result<string>.Success("ok");

        Assert.True(result.IsSuccess);
        Assert.Equal("ok", result.Value);
    }
}
