using System.Text;

namespace Veltis.Workspace.Application.Agents.Execution;

public sealed class ExecutionPromptBuilder : IExecutionPromptBuilder
{
    public string Build(AgentExecutionRequest request, string systemPrompt, string instructions)
    {
        var builder = new StringBuilder();

        builder.AppendLine("## Sistema");
        builder.AppendLine(systemPrompt);
        builder.AppendLine();

        builder.AppendLine("## Instruções");
        builder.AppendLine(instructions);
        builder.AppendLine();

        builder.AppendLine("## Dados informados pelo usuário");

        foreach (var item in request.FormValues)
        {
            builder.AppendLine($"- {item.Key}: {item.Value}");
        }

        builder.AppendLine();
        builder.AppendLine("Gere uma resposta profissional, clara e bem estruturada.");

        return builder.ToString();
    }
}