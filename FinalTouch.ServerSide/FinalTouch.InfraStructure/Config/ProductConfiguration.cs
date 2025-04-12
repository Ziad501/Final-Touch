using System;
using FinalTouch.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalTouch.InfraStructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Type).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Brand).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
    }
}
