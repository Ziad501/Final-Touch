using System;
using FinalTouch.Core.Entities;
using FinalTouch.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FinalTouch.InfraStructure.Data;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }
    

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(string? type,string? brand,string? sort)
    {
       var query = context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(type))
        {
            query = query.Where(p => p.Type == type);
        }
        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(p => p.Brand == brand);
        }
        if (!string.IsNullOrEmpty(sort))
        {
            query = sort switch
            {
                "price" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                "name" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                _ => query
            };
        }
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetBrandAsync()
    {
        return await context.Products.Select(p => p.Brand).Distinct().ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<IReadOnlyList<string>> GetTypeAsync()
    {
        return await context.Products.Select(p => p.Type).Distinct().ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
