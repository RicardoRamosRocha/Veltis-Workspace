namespace Veltis.Workspace.Domain.Common;

public interface IAuditableEntity
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    DateTime? DeletedAt { get; }
    bool IsDeleted { get; }
}
