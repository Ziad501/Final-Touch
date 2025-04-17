using System;
using System.IO;
using System.Text.Json;
using FinalTouch.Core.Entities;

namespace FinalTouch.InfraStructure.Data;

public class Seed
{
    public static async Task SeedDataAsync(AppDbContext context)
    {
        if(!context.Products.Any())
        {
            var ProductsData = await File.ReadAllTextAsync("../FinalTouch.InfraStructure/Data/SeedData/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
            if( Products == null) return;
            context.Products.AddRange(Products);
            await context.SaveChangesAsync();
        }
        if (!context.DeliveryMethods.Any())
        {
            var dmData = await File
                .ReadAllTextAsync("../FinalTouch.InfraStructure/Data/SeedData/delivery.json");

            var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

            if (methods == null) return;

            context.DeliveryMethods.AddRange(methods);

            await context.SaveChangesAsync();
        }
    }
}
