using Veltis.Workspace.Application.Common.Results;

namespace Veltis.Workspace.Application.Professions;

public interface IProfessionService
{
    Task<IReadOnlyCollection<ProfessionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ProfessionDto>> GetActiveAsync(CancellationToken cancellationToken = default);
    Task<ProfessionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<Guid>> CreateAsync(ProfessionInputDto input, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, ProfessionInputDto input, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
