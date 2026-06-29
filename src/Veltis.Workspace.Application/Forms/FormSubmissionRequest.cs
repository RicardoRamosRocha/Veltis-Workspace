namespace Veltis.Workspace.Application.Forms;

public sealed class FormSubmissionRequest
{
    public Guid FormDefinitionId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? WorkspaceId { get; set; }
    public IReadOnlyDictionary<string, string?> Values { get; set; } = new Dictionary<string, string?>();
    public bool IsPreview { get; set; }
}
