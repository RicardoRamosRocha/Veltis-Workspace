namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IAIProvider
{
    string Key { get; }
    Task<AiProviderResponse> ExecuteAsync(AiProviderRequest request, CancellationToken cancellationToken = default);
}
