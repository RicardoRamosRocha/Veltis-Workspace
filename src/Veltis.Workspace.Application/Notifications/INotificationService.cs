namespace Veltis.Workspace.Application.Notifications;

public interface INotificationService
{
    Task CreateInAppAsync(Guid? userId, string title, string message, CancellationToken cancellationToken = default);
}
