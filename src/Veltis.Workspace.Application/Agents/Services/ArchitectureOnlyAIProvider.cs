using Veltis.Workspace.Application.Agents.Interfaces;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class ArchitectureOnlyAIProvider : IAIProvider
{
    public string Key => "architecture-only";

    public Task<AiProviderResponse> ExecuteAsync(AiProviderRequest request, CancellationToken cancellationToken = default)
    {
        var response = new AiProviderResponse(
            "Execucao simulada para validacao arquitetural. Nenhum provedor externo foi acionado.",
            EstimateTokens(request.Prompt),
            12,
            TimeSpan.Zero,
            0m);

        return Task.FromResult(response);
    }

    private static int EstimateTokens(string text)
    {
        return Math.Max(1, text.Length / 4);
    }
}
