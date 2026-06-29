using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.Common.Results;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class AgentExecutor : IAgentExecutor
{
    private readonly IApplicationDbContext _context;
    private readonly IFormRenderer _formRenderer;
    private readonly IPromptBuilder _promptBuilder;
    private readonly IAIModelSelector _modelSelector;
    private readonly IAgentFactory _agentFactory;
    private readonly IAgentHistoryService _historyService;
    private readonly IDocumentGenerator _documentGenerator;

    public AgentExecutor(
        IApplicationDbContext context,
        IFormRenderer formRenderer,
        IPromptBuilder promptBuilder,
        IAIModelSelector modelSelector,
        IAgentFactory agentFactory,
        IAgentHistoryService historyService,
        IDocumentGenerator documentGenerator)
    {
        _context = context;
        _formRenderer = formRenderer;
        _promptBuilder = promptBuilder;
        _modelSelector = modelSelector;
        _agentFactory = agentFactory;
        _historyService = historyService;
        _documentGenerator = documentGenerator;
    }

    public async Task<Result<AgentExecutionResult>> ExecuteAsync(AgentExecutionRequest request, CancellationToken cancellationToken = default)
    {
        Agent? agent = await _context.Agents
            .Include(item => item.PromptTemplate)
            .Include(item => item.FormDefinition)
            .Include(item => item.DefaultModel)
            .ThenInclude(model => model!.AIProvider)
            .FirstOrDefaultAsync(item =>
                item.Id == request.AgentId
                && item.ProfessionId == request.ProfessionId
                && item.IsActive
                && !item.IsDeleted,
                cancellationToken);

        if (agent is null || agent.PromptTemplate is null || agent.FormDefinition is null)
        {
            return Result<AgentExecutionResult>.Failure("Agente nao encontrado ou incompleto.");
        }

        Result validation = _formRenderer.Validate(agent.FormDefinition, request.FormData);
        if (validation.IsFailure)
        {
            return Result<AgentExecutionResult>.Failure(validation.Errors);
        }

        RenderedPrompt prompt = await _promptBuilder.BuildAsync(
            new PromptBuildContext
            {
                Agent = agent,
                PromptTemplate = agent.PromptTemplate,
                FormData = request.FormData,
                UserPreferences = request.UserPreferences
            },
            cancellationToken);

        AIModel? model = await _modelSelector.SelectAsync(agent, cancellationToken);
        if (model is null)
        {
            return Result<AgentExecutionResult>.Failure("Nenhum modelo de IA ativo foi encontrado.");
        }

        string providerKey = model.AIProvider?.Key ?? model.Provider;
        IAIProvider provider = _agentFactory.GetProvider(providerKey);

        var stopwatch = Stopwatch.StartNew();
        AiProviderResponse providerResponse = await provider.ExecuteAsync(
            new AiProviderRequest(prompt.FinalPrompt, model.ModelName, agent.Temperature, agent.MaxTokens),
            cancellationToken);
        stopwatch.Stop();

        var execution = new AgentExecution
        {
            UserId = request.UserId,
            WorkspaceId = request.WorkspaceId,
            ProfessionId = request.ProfessionId,
            AgentId = request.AgentId,
            Prompt = prompt.FinalPrompt,
            Response = providerResponse.Content,
            Provider = providerKey,
            Model = model.ModelName,
            ExecutionTime = providerResponse.ExecutionTime == TimeSpan.Zero ? stopwatch.Elapsed : providerResponse.ExecutionTime,
            TokensInput = providerResponse.TokensInput,
            TokensOutput = providerResponse.TokensOutput,
            EstimatedCost = providerResponse.EstimatedCost
        };

        Guid executionId = await _historyService.SaveAsync(execution, cancellationToken);
        GeneratedDocument document = await _documentGenerator.GenerateAsync(execution, agent.Name, cancellationToken);

        return Result<AgentExecutionResult>.Success(new AgentExecutionResult(
            executionId,
            document.Id,
            providerResponse.Content,
            providerKey,
            model.ModelName,
            providerResponse.TokensInput,
            providerResponse.TokensOutput,
            providerResponse.EstimatedCost));
    }
}
