using Veltis.Workspace.Application.Agents.Interfaces;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class PromptRenderer : IPromptRenderer
{
    public string Render(string template, IReadOnlyDictionary<string, string> variables)
    {
        string rendered = template;

        foreach (KeyValuePair<string, string> variable in variables)
        {
            rendered = rendered.Replace($"{{{{{variable.Key}}}}}", variable.Value, StringComparison.OrdinalIgnoreCase);
        }

        return rendered;
    }
}
