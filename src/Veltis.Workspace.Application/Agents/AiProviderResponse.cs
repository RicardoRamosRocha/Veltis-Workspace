namespace Veltis.Workspace.Application.Agents;

public sealed record AiProviderResponse(
    string Content,
    int TokensInput,
    int TokensOutput,
    TimeSpan ExecutionTime,
    decimal EstimatedCost);
