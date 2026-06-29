using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Notifications;

public sealed class Notification : TenantEntity
{
    public Guid? UserId { get; set; }
    public NotificationType Type { get; set; } = NotificationType.InApp;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool Read { get; set; }
    public DateTime? ReadAt { get; set; }
}
