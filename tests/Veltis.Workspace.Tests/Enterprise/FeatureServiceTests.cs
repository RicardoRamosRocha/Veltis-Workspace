using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Application.FeatureFlags;
using Veltis.Workspace.Domain.FeatureFlags;

namespace Veltis.Workspace.Tests.Enterprise;

public sealed class FeatureServiceTests
{
    [Fact]
    public void FeatureService_Should_Implement_IFeatureService()
    {
        Assert.True(typeof(IFeatureService).IsAssignableFrom(typeof(FeatureService)));
    }

    [Fact]
    public void Feature_Should_Be_Disabled_By_Default()
    {
        var feature = new Feature { Key = FeatureKeys.Ai };

        Assert.False(feature.EnabledByDefault);
    }
}
