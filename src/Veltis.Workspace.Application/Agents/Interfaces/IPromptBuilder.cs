namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IPromptBuilder
{
    Task<RenderedPrompt> BuildAsync(PromptBuildContext context, CancellationToken cancellationToken = default);
}
