using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Application.FeatureFlags;

public sealed class FeatureService : IFeatureService
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;
    private readonly ITenantProvider _tenantProvider;

    public FeatureService(
        IApplicationDbContext context,
        ICacheService cacheService,
        ITenantProvider tenantProvider)
    {
        _context = context;
        _cacheService = cacheService;
        _tenantProvider = tenantProvider;
    }

    public async Task<bool> IsEnabledAsync(string featureKey, Guid? tenantId = null, CancellationToken cancellationToken = default)
    {
        Guid? resolvedTenantId = tenantId ?? _tenantProvider.TenantId;
        string cacheKey = $"feature:{resolvedTenantId?.ToString() ?? "global"}:{featureKey}";

        bool? cached = await _cacheService.GetAsync<bool?>(cacheKey, cancellationToken);
        if (cached.HasValue)
        {
            return cached.Value;
        }

        bool? tenantFlag = await _context.FeatureFlags
            .Where(flag => flag.FeatureKey == featureKey && flag.TenantId == resolvedTenantId && !flag.IsDeleted)
            .Select(flag => (bool?)flag.Enabled)
            .FirstOrDefaultAsync(cancellationToken);

        bool enabled = tenantFlag ?? await _context.Features
            .Where(feature => feature.Key == featureKey && !feature.IsDeleted)
            .Select(feature => feature.EnabledByDefault)
            .FirstOrDefaultAsync(cancellationToken);

        await _cacheService.SetAsync(cacheKey, enabled, TimeSpan.FromMinutes(5), cancellationToken);
        return enabled;
    }
}
