using Microsoft.Extensions.Logging;
using Veltis.Workspace.Application.Common.Interfaces;

namespace Veltis.Workspace.Infrastructure.Services;

public sealed class InMemoryJobQueue : IJobQueue
{
    private readonly ILogger<InMemoryJobQueue> _logger;

    public InMemoryJobQueue(ILogger<InMemoryJobQueue> logger)
    {
        _logger = logger;
    }

    public Task EnqueueAsync<TJob>(TJob job, CancellationToken cancellationToken = default)
        where TJob : IBackgroundJob
    {
        _logger.LogInformation("Background job {JobType} accepted by in-memory queue.", typeof(TJob).Name);
        return Task.CompletedTask;
    }
}
