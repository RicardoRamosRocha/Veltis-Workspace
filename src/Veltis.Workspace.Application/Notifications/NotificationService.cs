using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Notifications;

namespace Veltis.Workspace.Application.Notifications;

public sealed class NotificationService : INotificationService
{
    private readonly IApplicationDbContext _context;

    public NotificationService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateInAppAsync(Guid? userId, string title, string message, CancellationToken cancellationToken = default)
    {
        _context.Notifications.Add(new Notification
        {
            UserId = userId,
            Title = title,
            Message = message,
            Type = NotificationType.InApp
        });

        await _context.SaveChangesAsync(cancellationToken);
    }
}
