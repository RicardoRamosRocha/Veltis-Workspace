namespace Veltis.Workspace.Application.Agents;

public sealed record AiProviderRequest(string Prompt, string Model, decimal Temperature, int MaxTokens);
