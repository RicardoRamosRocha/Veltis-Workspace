using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Agents.Services;

namespace Veltis.Workspace.Tests.Agents;

public sealed class AgentFactoryTests
{
    [Fact]
    public void GetProvider_Should_Return_Registered_Provider()
    {
        IAIProvider provider = new ArchitectureOnlyAIProvider();
        var factory = new AgentFactory([provider]);

        IAIProvider resolved = factory.GetProvider("architecture-only");

        Assert.Same(provider, resolved);
    }

    [Fact]
    public void GetProvider_Should_Fail_For_Unknown_Provider()
    {
        var factory = new AgentFactory([]);

        Assert.Throws<InvalidOperationException>(() => factory.GetProvider("missing"));
    }
}
