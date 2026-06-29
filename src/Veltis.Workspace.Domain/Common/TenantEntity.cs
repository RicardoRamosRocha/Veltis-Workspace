namespace Veltis.Workspace.Domain.Common;

public abstract class TenantEntity : BaseEntity, ITenantEntity
{
    public Guid? TenantId { get; set; }
}
