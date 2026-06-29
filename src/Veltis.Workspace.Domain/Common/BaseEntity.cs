namespace Veltis.Workspace.Domain.Common;

public abstract class BaseEntity : IAuditableEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    public bool IsDeleted { get; protected set; }

    public void MarkAsCreated(DateTime utcNow)
    {
        CreatedAt = utcNow;
    }

    public void MarkAsUpdated(DateTime utcNow)
    {
        UpdatedAt = utcNow;
    }

    public void MarkAsDeleted(DateTime utcNow)
    {
        IsDeleted = true;
        DeletedAt = utcNow;
        UpdatedAt = utcNow;
    }

    public void Restore(DateTime utcNow)
    {
        IsDeleted = false;
        DeletedAt = null;
        UpdatedAt = utcNow;
    }
}
