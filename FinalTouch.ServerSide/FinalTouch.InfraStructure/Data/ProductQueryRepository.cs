using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalTouch.InfraStructure.Data;

public class ProductQueryRepository<T> : IProductQueryRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public ProductQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .ToListAsync();
    }

    public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await SpecificationEvaluator<T>
            .GetQuery(_context.Set<T>().AsQueryable(), spec)
            .CountAsync();
    }
}
