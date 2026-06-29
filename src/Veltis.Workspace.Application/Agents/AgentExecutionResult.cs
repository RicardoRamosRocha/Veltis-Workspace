namespace Veltis.Workspace.Application.Agents;

public sealed record AgentExecutionResult(
    Guid ExecutionId,
    Guid? DocumentId,
    string Response,
    string Provider,
    string Model,
    int TokensInput,
    int TokensOutput,
    decimal EstimatedCost);
