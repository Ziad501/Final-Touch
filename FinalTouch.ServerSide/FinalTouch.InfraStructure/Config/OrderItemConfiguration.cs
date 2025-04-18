using FinalTouch.Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.InfraStructure.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
       
            public void Configure(EntityTypeBuilder<OrderItem> builder)
            {
                builder.OwnsOne(x => x.ItemOrdered, o => o.WithOwner());
                builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            }
        }

}
