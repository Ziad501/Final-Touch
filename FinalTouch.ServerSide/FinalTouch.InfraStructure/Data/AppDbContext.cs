using System;
using Microsoft.EntityFrameworkCore;
using FinalTouch.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FinalTouch.InfraStructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

