using System;
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Interfaces;

public interface IProductGetterService
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync(string? type,string? brand, string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetTypeAsync();
    Task<IReadOnlyList<string>> GetBrandAsync();

}
