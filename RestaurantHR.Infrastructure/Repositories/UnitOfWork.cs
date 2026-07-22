using System.Collections.Concurrent;
using RestaurantHR.Application.Common.Interfaces;
using RestaurantHR.Infrastructure.Data;

namespace RestaurantHR.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var typeName = typeof(T).Name;
        return (IGenericRepository<T>)_repositories.GetOrAdd(typeName, _ => new GenericRepository<T>(_context));
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
