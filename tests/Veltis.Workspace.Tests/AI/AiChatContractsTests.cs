using Veltis.Workspace.Application.AI;

namespace Veltis.Workspace.Tests.AI;

public sealed class AiChatContractsTests
{
    [Fact]
    public void AiChatRequest_ShouldExposeExecutionParameters()
    {
        AiChatRequest request = new()
        {
            SystemPrompt = "system",
            UserPrompt = "user",
            Model = "gpt-4o-mini",
            Temperature = 0.4m,
            MaxTokens = 500
        };

        Assert.Equal("system", request.SystemPrompt);
        Assert.Equal("user", request.UserPrompt);
        Assert.Equal("gpt-4o-mini", request.Model);
        Assert.Equal(0.4m, request.Temperature);
        Assert.Equal(500, request.MaxTokens);
    }

    [Fact]
    public void AiChatResponse_ShouldExposeProviderMetadata()
    {
        AiChatResponse response = new()
        {
            Content = "content",
            Provider = "OpenAI",
            Model = "gpt-4o-mini",
            InputTokens = 10,
            OutputTokens = 20,
            EstimatedCost = 0.001m,
            Success = true
        };

        Assert.True(response.Success);
        Assert.Equal("OpenAI", response.Provider);
        Assert.Equal(10, response.InputTokens);
        Assert.Equal(20, response.OutputTokens);
        Assert.Equal(0.001m, response.EstimatedCost);
    }
}
