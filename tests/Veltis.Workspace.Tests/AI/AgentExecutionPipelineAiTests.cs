using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Veltis.Workspace.Application.Agents.Execution;
using Veltis.Workspace.Application.AI;

namespace Veltis.Workspace.Tests.AI;

public sealed class AgentExecutionPipelineAiTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldUseFallback_WhenApiKeyIsEmpty()
    {
        FakeAiChatProvider provider = new();
        AgentExecutionPipeline pipeline = CreatePipeline(provider, apiKey: string.Empty);

        AgentExecutionResult result = await pipeline.ExecuteAsync(CreateRequest());

        Assert.True(result.Success);
        Assert.False(provider.WasCalled);
        Assert.NotNull(result.FallbackMessage);
        Assert.Contains("modo simulado", result.Response);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldCallProvider_WhenApiKeyExists()
    {
        FakeAiChatProvider provider = new();
        AgentExecutionPipeline pipeline = CreatePipeline(provider, apiKey: "test-key");

        AgentExecutionResult result = await pipeline.ExecuteAsync(CreateRequest());

        Assert.True(provider.WasCalled);
        Assert.True(result.Success);
        Assert.Equal("OpenAI", result.Provider);
        Assert.Equal("gpt-4o-mini", result.Model);
        Assert.Equal(12, result.InputTokens);
        Assert.Equal(24, result.OutputTokens);
        Assert.Equal("Resposta fake da IA.", result.Response);
    }

    private static AgentExecutionPipeline CreatePipeline(FakeAiChatProvider provider, string apiKey)
    {
        return new AgentExecutionPipeline(
            new ExecutionPromptBuilder(),
            provider,
            Options.Create(new AiProviderOptions
            {
                DefaultProvider = "OpenAI",
                OpenAI = new OpenAiProviderSettings
                {
                    ApiKey = apiKey,
                    Model = "gpt-4o-mini",
                    BaseUrl = "https://api.openai.com/v1"
                }
            }),
            NullLogger<AgentExecutionPipeline>.Instance);
    }

    private static AgentExecutionRequest CreateRequest()
    {
        return new AgentExecutionRequest
        {
            UserId = Guid.NewGuid(),
            WorkspaceId = Guid.NewGuid(),
            ProfessionId = Guid.NewGuid(),
            AgentId = Guid.NewGuid(),
            FormValues = new Dictionary<string, string>
            {
                ["Título"] = "Teste",
                ["Descrição"] = "Descrição"
            }
        };
    }

    private sealed class FakeAiChatProvider : IAiChatProvider
    {
        public bool WasCalled { get; private set; }

        public Task<AiChatResponse> ExecuteAsync(AiChatRequest request, CancellationToken cancellationToken = default)
        {
            WasCalled = true;
            return Task.FromResult(new AiChatResponse
            {
                Content = "Resposta fake da IA.",
                Provider = "OpenAI",
                Model = request.Model,
                InputTokens = 12,
                OutputTokens = 24,
                EstimatedCost = 0.0001m,
                Success = true
            });
        }
    }
}
