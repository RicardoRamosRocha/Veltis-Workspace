using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Domain.Forms;

public sealed class FormSubmission : TenantEntity
{
    public Guid FormDefinitionId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? WorkspaceId { get; set; }
    public string ValuesJson { get; set; } = "{}";
    public string? ValidationJson { get; set; }
    public bool IsPreview { get; set; }
}
