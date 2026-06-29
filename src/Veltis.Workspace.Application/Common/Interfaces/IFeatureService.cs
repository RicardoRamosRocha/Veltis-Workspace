namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IFeatureService
{
    Task<bool> IsEnabledAsync(string featureKey, Guid? tenantId = null, CancellationToken cancellationToken = default);
}
