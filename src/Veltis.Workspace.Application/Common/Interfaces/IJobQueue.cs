namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IJobQueue
{
    Task EnqueueAsync<TJob>(TJob job, CancellationToken cancellationToken = default)
        where TJob : IBackgroundJob;
}
