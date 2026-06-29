namespace Veltis.Workspace.Application.AI;

public interface IAiChatProvider
{
    Task<AiChatResponse> ExecuteAsync(AiChatRequest request, CancellationToken cancellationToken = default);
}
