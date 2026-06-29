using Veltis.Workspace.Application.Agents;
using Veltis.Workspace.Application.Agents.Services;
using Veltis.Workspace.Domain.Agents;

namespace Veltis.Workspace.Tests.Agents;

public sealed class PromptBuilderTests
{
    [Fact]
    public async Task BuildAsync_Should_Compose_Generic_Prompt()
    {
        var builder = new PromptBuilder(new PromptRenderer());
        var context = new PromptBuildContext
        {
            Agent = new Agent { Name = "Agente generico" },
            PromptTemplate = new PromptTemplate
            {
                SystemPrompt = "Voce e um agente para {{topic}}.",
                Instructions = "Analise os dados.",
                OutputFormat = "Markdown"
            },
            FormData = new Dictionary<string, string> { ["topic"] = "produtividade" },
            UserPreferences = new Dictionary<string, string> { ["tone"] = "objetivo" },
            Rules = ["Nao inventar dados."]
        };

        RenderedPrompt prompt = await builder.BuildAsync(context);

        Assert.Contains("produtividade", prompt.SystemPrompt);
        Assert.Contains("Dados do formulario", prompt.FinalPrompt);
        Assert.Contains("Nao inventar dados.", prompt.FinalPrompt);
    }
}
