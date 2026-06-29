namespace Veltis.Workspace.Application.Agents.Interfaces;

public interface IAgentFactory
{
    IAIProvider GetProvider(string providerKey);
}
