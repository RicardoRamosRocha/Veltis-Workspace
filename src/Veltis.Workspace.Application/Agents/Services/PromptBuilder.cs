using System.Text;
using Veltis.Workspace.Application.Agents.Interfaces;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class PromptBuilder : IPromptBuilder
{
    private readonly IPromptRenderer _promptRenderer;

    public PromptBuilder(IPromptRenderer promptRenderer)
    {
        _promptRenderer = promptRenderer;
    }

    public Task<RenderedPrompt> BuildAsync(PromptBuildContext context, CancellationToken cancellationToken = default)
    {
        var variables = new Dictionary<string, string>(context.FormData, StringComparer.OrdinalIgnoreCase);

        foreach (KeyValuePair<string, string> preference in context.UserPreferences)
        {
            variables[$"user.{preference.Key}"] = preference.Value;
        }

        string systemPrompt = _promptRenderer.Render(context.PromptTemplate.SystemPrompt, variables);
        var finalPrompt = new StringBuilder();

        AppendSection(finalPrompt, "Prompt da profissao", context.ProfessionPrompt);
        AppendSection(finalPrompt, "Prompt do agente", context.PromptTemplate.Instructions);
        AppendSection(finalPrompt, "Dados do formulario", FormatDictionary(context.FormData));
        AppendSection(finalPrompt, "Preferencias do usuario", FormatDictionary(context.UserPreferences));
        AppendSection(finalPrompt, "Regras", string.Join(Environment.NewLine, context.Rules));
        AppendSection(finalPrompt, "Formato de saida", context.PromptTemplate.OutputFormat);

        return Task.FromResult(new RenderedPrompt(systemPrompt, _promptRenderer.Render(finalPrompt.ToString(), variables)));
    }

    private static void AppendSection(StringBuilder builder, string title, string? content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return;
        }

        builder.AppendLine($"## {title}");
        builder.AppendLine(content);
        builder.AppendLine();
    }

    private static string FormatDictionary(IReadOnlyDictionary<string, string> values)
    {
        if (values.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(Environment.NewLine, values.Select(item => $"- {item.Key}: {item.Value}"));
    }
}
