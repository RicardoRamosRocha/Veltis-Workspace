using Veltis.Workspace.Application.Agents.Interfaces;
using Veltis.Workspace.Application.Agents.Services;

namespace Veltis.Workspace.Tests.Agents;

public sealed class ModelSelectorTests
{
    [Fact]
    public void AIModelSelector_Should_Implement_Contract()
    {
        Assert.True(typeof(IAIModelSelector).IsAssignableFrom(typeof(AIModelSelector)));
    }
}
