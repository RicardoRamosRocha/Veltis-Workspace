using Veltis.Workspace.Application.Agents;
using Veltis.Workspace.Application.Agents.Services;

namespace Veltis.Workspace.Tests.Agents;

public sealed class PipelineTests
{
    [Fact]
    public async Task ArchitectureOnlyProvider_Should_Not_Call_External_Api()
    {
        var provider = new ArchitectureOnlyAIProvider();

        AiProviderResponse response = await provider.ExecuteAsync(
            new AiProviderRequest("Prompt de teste", "architecture-only-model", 0.2m, 100));

        Assert.Contains("simulada", response.Content);
        Assert.Equal(0m, response.EstimatedCost);
    }
}
