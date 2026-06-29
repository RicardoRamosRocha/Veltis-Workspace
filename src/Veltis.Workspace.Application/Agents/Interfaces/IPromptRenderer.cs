namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IPromptRenderer
{
    string Render(string template, IReadOnlyDictionary<string, string> variables);
}
