namespace Veltis.Workspace.Application.Agents.Execution;

public sealed class AgentExecutionPipeline : IAgentExecutionPipeline
{
    private readonly IExecutionPromptBuilder _promptBuilder;

    public AgentExecutionPipeline(IExecutionPromptBuilder promptBuilder)
    {
        _promptBuilder = promptBuilder;
    }

    public Task<AgentExecutionResult> ExecuteAsync(
        AgentExecutionRequest request,
        CancellationToken cancellationToken = default)
    {
        var prompt = _promptBuilder.Build(
            request,
            "Você é um agente profissional do Veltis Workspace.",
            "Transforme os dados enviados em um documento profissional.");

        var fakeResponse = """
        Documento gerado em modo simulado.

        Esta resposta ainda não foi criada por uma IA real.
        O objetivo desta sprint é validar o Prompt Builder e o Pipeline de Execução.
        """;

        var result = new AgentExecutionResult
        {
            Success = true,
            Prompt = prompt,
            Response = fakeResponse
        };

        return Task.FromResult(result);
    }
}