namespace Veltis.Workspace.Application.Professions;

public sealed record ProfessionDto(
    Guid Id,
    string Name,
    string? Description,
    string? Icon,
    string? Color,
    string Slug,
    bool Active);
