namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IBackgroundJob
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
