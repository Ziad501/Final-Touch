using System;
using Microsoft.EntityFrameworkCore;
using FinalTouch.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FinalTouch.Core.Entities.OrderAggregate;

namespace FinalTouch.InfraStructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrdersItems { get; set; }

    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

