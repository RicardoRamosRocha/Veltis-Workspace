using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Veltis.Workspace.Application.Common.Interfaces;
using Veltis.Workspace.Domain.Common;

namespace Veltis.Workspace.Infrastructure.Persistence;

public class Repository<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id && !entity.IsDeleted, cancellationToken);
    }

    public async Task<IReadOnlyCollection<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(entity => !entity.IsDeleted).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<T>> ListAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(entity => !entity.IsDeleted).Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.Where(entity => !entity.IsDeleted).AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<T, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet.Where(entity => !entity.IsDeleted);

        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.CountAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        entity.MarkAsDeleted(DateTime.UtcNow);
        _dbSet.Update(entity);
    }
}
