using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IRepository<T> : IReadRepository<T>
    where T : BaseEntity
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Remove(T entity);
}
