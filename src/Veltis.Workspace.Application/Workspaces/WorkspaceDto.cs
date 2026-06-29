namespace Veltis.Workspace.Application.Workspaces;

public sealed record WorkspaceDto(Guid Id, Guid UserId, string Name, string? Description);
