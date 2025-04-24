using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalTouch.InfraStructure.Data;

public class ProductCommandRepository<T> : IProductCommandRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public ProductCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public bool ProductExists(int id)
    {
        return _context.Set<T>().Any(e => e.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
