using Veltis.Workspace.Application.Agents.Interfaces;

namespace Veltis.Workspace.Application.Agents.Services;

public sealed class AgentFactory : IAgentFactory
{
    private readonly IEnumerable<IAIProvider> _providers;

    public AgentFactory(IEnumerable<IAIProvider> providers)
    {
        _providers = providers;
    }

    public IAIProvider GetProvider(string providerKey)
    {
        return _providers.FirstOrDefault(provider =>
            string.Equals(provider.Key, providerKey, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException($"AI provider '{providerKey}' is not registered.");
    }
}
