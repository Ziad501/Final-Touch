using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using System.Collections.Concurrent;

namespace FinalTouch.InfraStructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly ConcurrentDictionary<string, object> _commands = new();
    private readonly ConcurrentDictionary<string, object> _queries = new();

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductCommandRepository<TEntity> CommandRepository<TEntity>() where TEntity : BaseEntity
    {
        var key = typeof(TEntity).FullName!;

        return (IProductCommandRepository<TEntity>)_commands.GetOrAdd(key, _ =>
        {
            var repoType = typeof(ProductCommandRepository<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(repoType, _context)
                   ?? throw new InvalidOperationException($"Could not create command repo for {key}");
        });
    }

    public IProductQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : BaseEntity
    {
        var key = typeof(TEntity).FullName!;

        return (IProductQueryRepository<TEntity>)_queries.GetOrAdd(key, _ =>
        {
            var repoType = typeof(ProductQueryRepository<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(repoType, _context)
                   ?? throw new InvalidOperationException($"Could not create query repo for {key}");
        });
    }

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
