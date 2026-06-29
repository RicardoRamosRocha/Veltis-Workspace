namespace Veltis.Workspace.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IRepository<T> Repository<T>()
        where T : Veltis.Workspace.Domain.Common.BaseEntity;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
