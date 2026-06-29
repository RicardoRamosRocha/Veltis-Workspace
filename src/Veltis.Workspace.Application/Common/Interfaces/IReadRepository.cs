using System.Linq.Expressions;
using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IReadRepository<T>
    where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<T>> ListAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
}
