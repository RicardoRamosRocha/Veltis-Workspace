using Veltis.Workspace.Domain.Entities;

namespace Veltis.Workspace.Application.Professions;

public static class ProfessionMappings
{
    public static ProfessionDto ToDto(this Profession profession)
    {
        return new ProfessionDto(
            profession.Id,
            profession.Name,
            profession.Description,
            profession.Icon,
            profession.Color,
            profession.Slug,
            profession.Active);
    }
}
