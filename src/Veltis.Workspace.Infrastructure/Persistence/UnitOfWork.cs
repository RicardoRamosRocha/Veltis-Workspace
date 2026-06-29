using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>()
        where T : BaseEntity
    {
        Type entityType = typeof(T);

        if (!_repositories.TryGetValue(entityType, out object? repository))
        {
            repository = new Repository<T>(_context);
            _repositories.Add(entityType, repository);
        }

        return (IRepository<T>)repository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
